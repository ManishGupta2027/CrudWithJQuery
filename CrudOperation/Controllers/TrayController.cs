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
            var resp = _trayService.UpsertTray(Guid.Parse("30231bed-c539-41f0-a369-c895da36566c"), Guid.Parse("30231bed-c539-41f0-a369-c895da36566c"), mappedModel, "Avi");
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
            var resp = _trayService.UpsertTray(Guid.Parse("30231bed-c539-41f0-a369-c895da36566c"), Guid.Parse("30231bed-c539-41f0-a369-c895da36566c"), mappedModel,string.Empty);
            return Json(resp, JsonRequestBehavior.DenyGet);
        }
        [HttpPost]
        public JsonResult GetTrays(Tray model)
        {
            model.CurrentPage = model.CurrentPage != 0 ? model.CurrentPage : 1;
            model.PageSize = model.PageSize != 0 ? model.PageSize : 40;//ConfigKeys.PageSize;
            var resp = _trayService.GetTrays(Guid.Parse("30231bed-c539-41f0-a369-c895da36566c"), Guid.Parse("30231bed-c539-41f0-a369-c895da36566c"), model.CurrentPage, model.PageSize, model.Name, model.DeliveryCenterId);
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
     





    }
}