using CrudOperation.Data;
using CrudOperation.Entities;
using CrudOperation.Entities.Trays;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrudOperation.Repository.Tray
{
    public class TrayRepository: ITrayRepository
    {
        private readonly OMSDBDataContext _dbcontext;
        public TrayRepository(IDataFactory dataContextFactory)
        {
            _dbcontext = dataContextFactory.OMSDBDataContext();
        }
        public BoolResponse DeleteTray(Guid orgId, Guid domainId, Guid id)
        {
            var result = new BoolResponse();
            var dbResponse = _dbcontext.procDeleteTray_20241118(orgId, domainId, id);
            result = (from o in dbResponse
                      select new BoolResponse
                      {
                          RecordId = o.RecordId ?? Guid.Empty,
                          Message = o.Message,
                          IsValid = o.IsValid.GetValueOrDefault()
                      }).FirstOrDefault();
            return result;
        }

        public Entities.Trays.Tray GetTrayDetail(Guid orgId, Guid domainId, Guid id)
        {
            var dbResponse = _dbcontext.procGetTrayDetails_20241118(orgId, domainId, id);
            var tray = (from o in dbResponse
                        select new Entities.Trays.Tray
                        {
                            Name = o.Name,
                            BarCode = o.BarCode,
                            Code = o.Code,
                            DeliveryCenterId = o.DeliveryCenterId,
                            RecordId = o.RecordId,
                        }).FirstOrDefault();
            return tray;
        }

        public List<TrayLabelPDF> GetTrayLabelPdfData(Guid orgId, List<TrayForBarCodePrint> tray)
        {
            var trayBarcode = new List<TrayLabelPDF>();
            var camelCaseFormatter = new JsonSerializerSettings();
            camelCaseFormatter.ContractResolver = new CamelCasePropertyNamesContractResolver();
            var trayJson = JsonConvert.SerializeObject(tray, camelCaseFormatter);
            var dbResponse = _dbcontext.procGetTrayBarCodePDFData_20241121(orgId, trayJson);
            var response = dbResponse.FirstOrDefault();
            if (response?.JsonResult != null)
                trayBarcode = JsonConvert.DeserializeObject<List<TrayLabelPDF>>(response.JsonResult);
            return trayBarcode;
        }

        public IList<Entities.Trays.Tray> GetTrays(Guid orgId, Guid domainId, int? currentPage = null, int? pageSize = null, string name = null, Guid? deliveryCenterId = null)
        {
            var dbResponse = _dbcontext.procGetTrayList_20241118(orgId, domainId, name, deliveryCenterId, currentPage, pageSize);
            var tray = (from o in dbResponse
                        select new Entities.Trays.Tray
                        {
                            Name = o.Name,
                            BarCode = o.BarCode,
                            Code = o.Code,
                            RecordId = o.RecordId,
                            Created = o.Created,
                            CreatedBy = o.CreatedBy,
                            CurrentPage = currentPage ?? 1,
                            PageSize = pageSize ?? 40,//Entities.Common.ConfigKeys.PageSize,
                            TotalRecord = o.TotalRecords ?? 0
                        }).ToList();
            return tray;
        }

        public BoolResponse UpdateTrayBarCode(Guid orgId, Guid domainId, Guid id, string barCodeUrl, string savedBy)
        {
            var result = new BoolResponse();
            var dbResponse = _dbcontext.procUpdateTrayBarCodeUrl_20241118(orgId, domainId, id, barCodeUrl, savedBy);
            result = (from o in dbResponse
                      select new BoolResponse
                      {
                          RecordId = o.RecordId ?? Guid.Empty,
                          Message = o.Message,
                          IsValid = o.IsValid.GetValueOrDefault()
                      }).FirstOrDefault();
            return result;
        }

        public BoolResponse UpsertTray(Guid orgId, Guid domainId, Entities.Trays.Tray model, string savedBy)
        {
            var result = new BoolResponse();
            var dbResponse = _dbcontext.procUpsertTray_20241118(orgId, domainId, model.RecordId, model.Name, model.BarCode, model.DeliveryCenterId, savedBy);
            result = (from o in dbResponse
                      select new BoolResponse
                      {
                          RecordId = o.RecordId ?? Guid.Empty,
                          Message = o.Message,
                          IsValid = o.IsValid.GetValueOrDefault()
                      }).FirstOrDefault();
            return result;
        }
    }
}
