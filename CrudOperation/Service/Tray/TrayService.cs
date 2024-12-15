using CrudOperation.Entities;
using CrudOperation.Entities.Trays;
using CrudOperation.Models.Tray;
using CrudOperation.Repository.Tray;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrudOperation.Service.Tray
{
    public class TrayService : ITrayService
    {
        private readonly ITrayRepository _trayRepository;
        public TrayService(ITrayRepository trayRepository)
        {
            _trayRepository = trayRepository;
        }

        public BoolResponse DeleteTray(Guid orgId, Guid domainId, Guid id)
        {
            return _trayRepository.DeleteTray(orgId, domainId, id);
        }

        public Entities.Trays.Tray GetTrayDetail(Guid orgId, Guid domainId, Guid id)
        {
            return _trayRepository.GetTrayDetail(orgId, domainId, id);
        }

        public List<TrayLabelPDF> GetTrayLabelPdfData(Guid orgId, List<TrayForBarCodePrint> tray)
        {
            return _trayRepository.GetTrayLabelPdfData(orgId, tray);
        }

        public IList<Entities.Trays.Tray> GetTrays(Guid orgId, Guid domainId, int? currentPage = null, int? pageSize = null, string name = null, Guid? deliveryCenterId = null)
        {
            return _trayRepository.GetTrays(orgId, domainId, currentPage, pageSize, name, deliveryCenterId);
        }

        public BoolResponse UpdateTrayBarCode(Guid orgId, Guid domainId, Guid id, string barCodeUrl, string savedBy)
        {
            return _trayRepository.UpdateTrayBarCode(orgId, domainId, id, barCodeUrl, savedBy);
        }

        public BoolResponse UpsertTray(Guid orgId, Guid domainId, Entities.Trays.Tray model, string savedBy)
        {
            return _trayRepository.UpsertTray(orgId, domainId, model, savedBy);
        }

        public IList<TrayGroup> GetTrayGroups(Guid orgId, Guid domainId, Guid deliveryCenterId, int? currentPage = null, int? pageSize = null, string name = null)
        {
            return _trayRepository.GetTrayGroups(orgId, domainId, deliveryCenterId, currentPage, pageSize, name);
        }

        public BoolResponse DeleteTrayGroup(Guid orgId, Guid domainId, Guid recordId, string savedBy)
        {
            return _trayRepository.DeleteTrayGroup(orgId, domainId, recordId, savedBy);
        }

        public BoolResponse UpsertTrayGroup(Guid orgId, Guid domainId, TrayGroup model, string savedBy)
        {
            return _trayRepository.UpsertTrayGroup(orgId, domainId, model, savedBy);
        }

        public TrayGroup GetTrayGroupDetail(Guid orgId, Guid domainId, Guid id)
        {
            return _trayRepository.GetTrayGroupDetail(orgId, domainId, id);
        }

        public IList<TrayListModel> GetAvailableTrays(Guid orgId, Guid domainId, Guid deliveryCenterId, int? currentPage = null, int? pageSize = null, string name = null)
        {
            return _trayRepository.GetAvailableTrays(orgId, domainId, deliveryCenterId,currentPage,pageSize,name);
        }
    }
}