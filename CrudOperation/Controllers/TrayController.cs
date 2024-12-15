using AutoMapper;
using CrudOperation.Entities.Trays;
using CrudOperation.Models.Tray;
using CrudOperation.Service.Tray;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Diagnostics;
using CrudOperation.Helper;
using CrudOperation.Entities;
using CrudOperation.Entities.Common;
using System.Web.Services.Description;
using CrudOperation.Models;
using System.Collections;

namespace CrudOperation.Controllers
{
    public class TrayController : Controller
    {
        private readonly ITrayService _trayService;
        public TrayController(ITrayService trayService)
        {
            _trayService=trayService;
        }
        private DeliveryCenterLocationsModel GetDeliveryCenterDetailModel(Guid deliveryCenterId)
        {
            // Find the delivery center by RecordId
            var deliveryCenter = DemoData.DeliveryCenters.FirstOrDefault(dc => dc.DeliveryCenterId == deliveryCenterId);

          //  var delCenter = DemoData.DeliveryCenters.FirstOrDefault();// _deliveryCenterService.GetDeliveryCenterDetail(deliveryCenterId);
            return new DeliveryCenterLocationsModel
            {
                DeliveryCenterId = deliveryCenter.DeliveryCenterId,
                DeliveryCenterName = deliveryCenter.DeliveryCenterName
            };
        }
        public ActionResult Trays()
        {
            return View();
        }
        public ActionResult AddTray()
        {
            return View();
        }
        public ActionResult DetailTray(Guid id)
        {
            //var containerName = BlobConstants.OMS_BASE_FOLDER;
            byte[] downloadedData = null;
            // Retrieve the AutoMapper instance
            var mapper = (IMapper)HttpContext.Application["Mapper"];
            var resp = _trayService.GetTrayDetail(Guid.Parse("30231bed-c539-41f0-a369-c895da36566c"), Guid.Parse("30231bed-c539-41f0-a369-c895da36566c"), id);
            var mappedModel = mapper.Map<TrayDetailModel>(resp);
            //if (!string.IsNullOrEmpty(mappedModel.BarCode))
            //{
            //    if (!mappedModel.BarCode.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            //    {
            //        //get Org settings
            //        var orgSetting = _orgService.GetOrg(_sessionContext.OrgId);
            //        //if org uses general blob then add OrgCode as prefix with container name
            //        if (orgSetting.UseGeneralBlob)
            //            containerName = orgSetting.OrgCode.Replace(" ", "_").ToLower() + "-" + BlobConstants.OMS_BASE_FOLDER;
            //        var fileName = BlobConstants.TRAY_FOLDER + "/" + mappedModel.Code;
            //        mappedModel.BarCode = _commonService.UploadBarCode(fileName, containerName);
            //        //Update barCodeUrl
            //        if (!string.IsNullOrEmpty(mappedModel.BarCode))
            //            _trayService.UpdateTrayBarCode(_sessionContext.OrgId, _sessionContext.CurrentDomainId, mappedModel.RecordId, mappedModel.BarCode, _sessionContext.User.Email);
            //    }


            //    downloadedData = DownloadByte(mappedModel.BarCode, Services.Helper.Utils.DomainStorageSettings.StorageConnectionString);

            //    // Convert the downloaded data (byte array) into Base64
            //    string base64 = Convert.ToBase64String(downloadedData);

            //    // Assign the Base64 string to the model
            //    mappedModel.Base64 = base64;
            //}
            return View(mappedModel);
        }
        [HttpPost]
        public JsonResult SaveTray(TrayAddModel model)
        {
            // var mappedModel = Mapper.Map<Tray>(model);
            // Retrieve the AutoMapper instance
            var mapper = (IMapper)HttpContext.Application["Mapper"];

            // Map the TrayAddModel to Tray
            var mappedModel = mapper.Map<Tray>(model);
            var resp = _trayService.UpsertTray(DemoData._sessionContext.OrgId, DemoData._sessionContext.CurrentDomainId, mappedModel, "Avi");
            return JsonHelper.JsonSuccess(resp, JsonRequestBehavior.DenyGet);
        }
        [HttpPut]
        public JsonResult UpdateTray(TrayUpdateModel model)
        {
            // Retrieve the AutoMapper instance
            var mapper = (IMapper)HttpContext.Application["Mapper"];
            var mappedModel = mapper.Map<Tray>(model);
            // Ensure RecordId is properly set
            if (mappedModel.RecordId == Guid.Empty)
                mappedModel.RecordId = model.RecordId;
            var resp = _trayService.UpsertTray(DemoData._sessionContext.OrgId, DemoData._sessionContext.CurrentDomainId, mappedModel,string.Empty);
            return Json(resp, JsonRequestBehavior.DenyGet);
        }
        [HttpPost]
        public JsonResult GetTrays(Tray model)
        {
            model.CurrentPage = model.CurrentPage != 0 ? model.CurrentPage : 1;
            model.PageSize = model.PageSize != 0 ? model.PageSize : 40;//ConfigKeys.PageSize;
            var resp = _trayService.GetTrays(DemoData._sessionContext.OrgId, DemoData._sessionContext.CurrentDomainId, model.CurrentPage, model.PageSize, model.Name, model.DeliveryCenterId);
            // Debug the response here
            Debug.WriteLine(JsonConvert.SerializeObject(resp, Formatting.Indented));

            return JsonHelper.JsonSuccess(resp, JsonRequestBehavior.DenyGet);
            // return JsonSuccess(resp);
        }
        //[HttpDelete]
        //public JsonResult DeleteTray(Guid id)
        //{
        //    var resp = _trayService.DeleteTray(_sessionContext.OrgId, _sessionContext.CurrentDomainId, id);
        //    return JsonSuccess(resp, JsonRequestBehavior.DenyGet);
        //}
        [HttpGet]
        public ActionResult GetAllWarehouses()
        {
            //var wareHouses = _deliveryCenterService.GetDeliveryCenters(Entities.Enums.DeliveryCenterTypes.Any);
            //var mappedModel = Mapper.Map<List<UserDeliveryCenterModel>>(wareHouses).ToList();
            //return JsonSuccess(mappedModel, JsonRequestBehavior.AllowGet);
            // Create dummy data list
            var warehousesList = new List<object>
                {
                    new { RecordId = Guid.Parse("30231bed-c539-41f0-a369-c895da36566c"), Code = "WH001" },
                    new { RecordId = Guid.Parse("28e8b8da-63a4-4dde-b6f9-10ea131d74de"), Code = "WH002" },
                    new { RecordId = Guid.Parse("cf21d9e0-1b2a-46b5-b199-aae123e4ee74"), Code = "WH003" },
                    new { RecordId = Guid.Parse("0e2b1e29-0033-4d32-aa02-126f61cd58e8"), Code = "WH004" }
                };

            // Return the list as JSON
            return JsonHelper.JsonSuccess(warehousesList, JsonRequestBehavior.AllowGet);
            //return Json(new { success = true, data = warehousesList }, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public ActionResult SetTrayForBarCodePrint(List<TrayForBarCodePrint> tray)
        {
            var resp = new BoolResponse();
            resp.IsValid = true;
            if (tray != null && tray.Any())
                TempData["trayforBarcCodePrint"] = tray;
            else
                resp.Message = "Please select tray.";
            return JsonHelper.JsonSuccess(new { result = resp }, JsonRequestBehavior.DenyGet);
        }

        //public JsonResult JsonSuccess(object data)
        //{
        //    var settings = new JsonSerializerSettings
        //    {
        //        ContractResolver = new CamelCasePropertyNamesContractResolver(),
        //        Formatting = Formatting.Indented // Optional for pretty printing
        //    };

        //    // Serialize the object to JSON
        //    var json = JsonConvert.SerializeObject(data, settings);

        //    // Create and return a JsonResult
        //    return new JsonResult
        //    {
        //        Data = JsonConvert.DeserializeObject(json), // Deserialize to ensure compatibility
        //        JsonRequestBehavior = JsonRequestBehavior.AllowGet
        //    };
        //}


        #region TrayGroup
        public ActionResult TrayGroups(Guid deliveryCenterId)
        {
            var model = GetDeliveryCenterDetailModel(deliveryCenterId);
            return View(model);
        }
        [HttpPost]
        public JsonResult TrayGroupList(TrayGroup model)
        {
            model.CurrentPage = model.CurrentPage != 0 ? model.CurrentPage : 1;
            model.PageSize = model.PageSize != 0 ? model.PageSize : 40;
            var resp = _trayService.GetTrayGroups(DemoData._sessionContext.OrgId, DemoData._sessionContext.CurrentDomainId, model.DeliveryCenterId, model.CurrentPage, model.PageSize, model.Name);
            return JsonHelper.JsonSuccess(resp, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AddTrayGroup(Guid deliveryCenterId)
        {
            var model = GetDeliveryCenterDetailModel(deliveryCenterId);
            return View(model);
        }
        [HttpDelete]
        public JsonResult DeleteTrayGroup(Guid id)
        {
            var response = _trayService.DeleteTrayGroup(DemoData._sessionContext.OrgId, DemoData._sessionContext.CurrentDomainId, id, DemoData._sessionContext.UserEmail);
            return JsonHelper.JsonSuccess(response, JsonRequestBehavior.DenyGet);
        }
        [HttpPost]
        public JsonResult SaveTrayGroup(TrayGroupAddModel model)
        {
            var mapper = (IMapper)HttpContext.Application["Mapper"];
            var mappedModel = mapper.Map<TrayGroup>(model);
            var resp = _trayService.UpsertTrayGroup(DemoData._sessionContext.OrgId, DemoData._sessionContext.CurrentDomainId, mappedModel, DemoData._sessionContext.UserEmail);
            return JsonHelper.JsonSuccess(resp, JsonRequestBehavior.DenyGet);
        }
        [HttpPut]
        public JsonResult UpdateTrayGroup(TrayGroupUpdateModel model)
        {
            // Retrieve the AutoMapper instance
            var mapper = (IMapper)HttpContext.Application["Mapper"];
            var mappedModel = mapper.Map<TrayGroup>(model);
            // Ensure RecordId is properly set
            if (mappedModel.RecordId == Guid.Empty)
                mappedModel.RecordId = model.RecordId;
            var resp = _trayService.UpsertTrayGroup(DemoData._sessionContext.OrgId, DemoData._sessionContext.CurrentDomainId, mappedModel, DemoData._sessionContext.UserEmail);
            return JsonHelper.JsonSuccess(resp, JsonRequestBehavior.DenyGet);
        }
        public ActionResult TrayGroupDetail(Guid id)
        {
            var mapper = (IMapper)HttpContext.Application["Mapper"];
            var containerName = "v3";//BlobConstants.OMS_BASE_FOLDER;
            byte[] downloadedData = null;
            var resp = _trayService.GetTrayGroupDetail(DemoData._sessionContext.OrgId, DemoData._sessionContext.CurrentDomainId, id);
            var mappedModel = mapper.Map<TrayGroupDetailModel>(resp);
            //if (!string.IsNullOrEmpty(mappedModel.BarCode))
            //{
            //    if (!mappedModel.BarCode.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            //    {
            //        //get Org settings
            //        var orgSetting = _orgService.GetOrg(DemoData._sessionContext.OrgId);
            //        //if org uses general blob then add OrgCode as prefix with container name
            //        if (orgSetting.UseGeneralBlob)
            //            containerName = orgSetting.OrgCode.Replace(" ", "_").ToLower() + "-" + BlobConstants.OMS_BASE_FOLDER;
            //        var fileName = BlobConstants.TRAY_FOLDER + "/" + mappedModel.Code;
            //        mappedModel.BarCode = _commonService.UploadBarCode(fileName, containerName);
            //        //Update barCodeUrl
            //        if (!string.IsNullOrEmpty(mappedModel.BarCode))
            //            _trayService.UpdateTrayBarCode(DemoData._sessionContext.OrgId, DemoData._sessionContext.CurrentDomainId, mappedModel.RecordId, mappedModel.BarCode, DemoData._sessionContext.UserEmail);
            //    }


            //    downloadedData = DownloadByte(mappedModel.BarCode, Services.Helper.Utils.DomainStorageSettings.StorageConnectionString);

            //    // Convert the downloaded data (byte array) into Base64
            //    string base64 = Convert.ToBase64String(downloadedData);

            //    // Assign the Base64 string to the model
            //    mappedModel.Base64 = base64;

            //    var delCenter = GetDeliveryCenterDetailModel(mappedModel.DeliveryCenterId);
            //    mappedModel.DeliveryCenterName = delCenter.DeliveryCenterName;
            //}
            return View(mappedModel);
        }


        /// <summary>
        /// Searches the tray list by name.
        /// </summary>
        /// <param name="name">The name to search for.</param>
        /// <returns>A list of trays that match the search term.</returns>
        /// 
        [HttpGet]
        public JsonResult GetTrayList(string name)
        {
            // If name is null or empty, return all trays
            if (string.IsNullOrWhiteSpace(name))
            {
                return JsonHelper.JsonSuccess( DemoData.TrayList ,JsonRequestBehavior.AllowGet);
            }

            // Perform case-insensitive search
            //var list= DemoData.TrayList
            //    .Where(tray => tray.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
            //      return DemoData.TrayList
            //.Where(tray => tray.Name != null && tray.Name.ToLower().Contains(name.ToLower())).ToList();
            // Perform case-insensitive exact match
            var list= DemoData.TrayList
                .Where(tray => !string.IsNullOrEmpty(tray.Name) &&
                               string.Equals(tray.Name, name, StringComparison.OrdinalIgnoreCase))
                .ToList();
            return JsonHelper.JsonSuccess(list, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult GetAvailableTrays(TrayGroup model)
        {
            
            model.CurrentPage = model.CurrentPage != 0 ? model.CurrentPage : 1;
            model.PageSize = model.PageSize != 0 ? model.PageSize : 40;
            var resp = _trayService.GetAvailableTrays(DemoData._sessionContext.OrgId, DemoData._sessionContext.CurrentDomainId, model.DeliveryCenterId, model.CurrentPage, model.PageSize, model.Name);
            return JsonHelper.JsonSuccess(resp, JsonRequestBehavior.AllowGet);

        }
        #endregion



    }
}