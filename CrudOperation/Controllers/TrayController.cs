using CrudOperation.Service.Tray;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CrudOperation.Controllers
{
    public class TrayController : Controller
    {
        private readonly ITrayService _trayService;
        public TrayController(ITrayService trayService)
        {
            _trayService=trayService;
        }

        public ActionResult Trays()
        {
            return View();
        }
        public ActionResult AddTray()
        {
            return View();
        }
        //public ActionResult DetailTray(Guid id)
        //{
        //    var containerName = BlobConstants.OMS_BASE_FOLDER;
        //    byte[] downloadedData = null;
        //    var resp = _trayService.GetTrayDetail(_sessionContext.OrgId, _sessionContext.CurrentDomainId, id);
        //    var mappedModel = Mapper.Map<TrayDetailModel>(resp);
        //    if (!string.IsNullOrEmpty(mappedModel.BarCode))
        //    {
        //        if (!mappedModel.BarCode.StartsWith("https", StringComparison.OrdinalIgnoreCase))
        //        {
        //            //get Org settings
        //            var orgSetting = _orgService.GetOrg(_sessionContext.OrgId);
        //            //if org uses general blob then add OrgCode as prefix with container name
        //            if (orgSetting.UseGeneralBlob)
        //                containerName = orgSetting.OrgCode.Replace(" ", "_").ToLower() + "-" + BlobConstants.OMS_BASE_FOLDER;
        //            var fileName = BlobConstants.TRAY_FOLDER + "/" + mappedModel.Code;
        //            mappedModel.BarCode = _commonService.UploadBarCode(fileName, containerName);
        //            //Update barCodeUrl
        //            if (!string.IsNullOrEmpty(mappedModel.BarCode))
        //                _trayService.UpdateTrayBarCode(_sessionContext.OrgId, _sessionContext.CurrentDomainId, mappedModel.RecordId, mappedModel.BarCode, _sessionContext.User.Email);
        //        }


        //        downloadedData = DownloadByte(mappedModel.BarCode, Services.Helper.Utils.DomainStorageSettings.StorageConnectionString);

        //        // Convert the downloaded data (byte array) into Base64
        //        string base64 = Convert.ToBase64String(downloadedData);

        //        // Assign the Base64 string to the model
        //        mappedModel.Base64 = base64;
        //    }
        //    return View("~/Views/Setting/TrayDetail.cshtml", mappedModel);
        //}
        //[HttpPost]
        //public JsonResult SaveTray(TrayAddModel model)
        //{
        //    var mappedModel = Mapper.Map<Tray>(model);
        //    var resp = _trayService.UpsertTray(_sessionContext.OrgId, _sessionContext.CurrentDomainId, mappedModel, _sessionContext.User.Email);
        //    return JsonSuccess(resp, JsonRequestBehavior.DenyGet);
        //}
        //[HttpPut]
        //public JsonResult UpdateTray(TrayUpdateModel model)
        //{
        //    var mappedModel = Mapper.Map<Tray>(model);
        //    // Ensure RecordId is properly set
        //    if (mappedModel.RecordId == Guid.Empty)
        //        mappedModel.RecordId = model.RecordId;
        //    var resp = _trayService.UpsertTray(_sessionContext.OrgId, _sessionContext.CurrentDomainId, mappedModel, _sessionContext.User.Email);
        //    return JsonSuccess(resp, JsonRequestBehavior.DenyGet);
        //}
        //[HttpPost]
        //public JsonResult GetTrays(Tray model)
        //{
        //    model.CurrentPage = model.CurrentPage != 0 ? model.CurrentPage : 1;
        //    model.PageSize = model.PageSize != 0 ? model.PageSize : ConfigKeys.PageSize;
        //    var resp = _trayService.GetTrays(_sessionContext.OrgId, _sessionContext.CurrentDomainId, model.CurrentPage, model.PageSize, model.Name, model.DeliveryCenterId);
        //    return JsonSuccess(resp, JsonRequestBehavior.AllowGet);
        //}
        //[HttpDelete]
        //public JsonResult DeleteTray(Guid id)
        //{
        //    var resp = _trayService.DeleteTray(_sessionContext.OrgId, _sessionContext.CurrentDomainId, id);
        //    return JsonSuccess(resp, JsonRequestBehavior.DenyGet);
        //}
        //[HttpGet]
        //public ActionResult GetAllWarehouses()
        //{
        //    var wareHouses = _deliveryCenterService.GetDeliveryCenters(Entities.Enums.DeliveryCenterTypes.Any);
        //    var mappedModel = Mapper.Map<List<UserDeliveryCenterModel>>(wareHouses).ToList();
        //    return JsonSuccess(mappedModel, JsonRequestBehavior.AllowGet);
        //}
        //[HttpPost]
        //public ActionResult SetTrayForBarCodePrint(List<TrayForBarCodePrint> tray)
        //{
        //    var resp = new BoolResponse();
        //    resp.IsValid = true;
        //    if (tray != null && tray.Any())
        //        TempData["trayforBarcCodePrint"] = tray;
        //    else
        //        resp.Message = "Please select tray.";
        //    return JsonSuccess(new { result = resp }, JsonRequestBehavior.DenyGet);
        //}
    }
}