using AutoMapper;
using CrudOperation.Entities;
using CrudOperation.Entities.Trays;
using CrudOperation.Repository.Tray;
using CrudOperation.Service.Content;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace CrudOperation.Controllers
{
    public class ContentController : Controller
    {
        private ITrayRepository _trayRepository;
        private IContentService _contentService;
        public ContentController(ITrayRepository trayRepository, IContentService contentService)
        {
            _trayRepository=trayRepository;
            _contentService = contentService;
    }
        // GET: Content
        public FileContentResult DownloadPdf(JsReportPdfDocumentTypes documentType, string id)
        {
            var templateModel = new JsReportPdfTemplate();
            var dataEntity = new DataEntity();
            var fileName = string.Empty;
            var recordId = Guid.Empty;
           // var reportSetting = _settingService.GetJsReportSetting(_sessionContext.OrgId, _sessionContext.CurrentDomainId, documentType);
            switch (documentType)
            {
                //case JsReportPdfDocumentTypes.IN:
                //    bool printReturnSheet = false;
                //    bool printReturnLabel = false;
                //    bool displayLogoOnInvoice = false;
                //    var settings = _settingService.GetStoreSettingByType(_sessionContext.OrgId, StoreSettingTypes.GeneralSettings);
                //    if (settings?.SettingValues != null)
                //    {
                //        var generalSetting = JsonConvert.DeserializeObject<StoreGeneralSettings>(settings.SettingValues);
                //        printReturnSheet = generalSetting.PrintReturnSheet;
                //        printReturnLabel = generalSetting.PrintReturnLabel;
                //        displayLogoOnInvoice = generalSetting.DisplayLogoOnInvoice;
                //    }
                //    var invoiceDetail = _orderService.GetInvoiceByPlanIds(_sessionContext.OrgId, id);
                //    var domainIds = invoiceDetail.Select(x => x.DomainId).Distinct().ToList();
                //    var domains = new List<Omnicx.Backend.Framework.Entities.Domain>();
                //    var b2bDomainId = Guid.Empty;
                //    var b2cDomainId = Guid.Empty;
                //    foreach (var d in domainIds)
                //    {

                //        var domain = _sessionContext.OrgSettings.Domains.Where(x => x.Id == d).FirstOrDefault();
                //        if (domain.B2BSettings.EnableB2B)
                //            b2bDomainId = domain.Id;
                //        else
                //            b2cDomainId = domain.Id;
                //    }
                //    var b2bInvoices = invoiceDetail.Where(x => x.DomainId == b2bDomainId);
                //    var b2cInvoices = invoiceDetail.Where(x => x.DomainId == b2cDomainId);
                //    var invoiceB2CModel = new List<InvoiceB2CReportModel>();
                //    var invoiceB2BModel = new List<InvoiceB2BReportModel>();
                //    if (b2cInvoices != null && b2cInvoices.Count() > 0)
                //    {
                //        invoiceB2CModel = Mapper.Map<List<InvoiceB2CReportModel>>(b2cInvoices);
                //        dataEntity.Entity = invoiceB2CModel;
                //    }
                //    if (b2bInvoices != null && b2bInvoices.Count() > 0)
                //    {
                //        reportSetting = _settingService.GetJsReportSetting(_sessionContext.OrgId, _sessionContext.CurrentDomainId, JsReportPdfDocumentTypes.INB);
                //        invoiceB2BModel = Mapper.Map<List<InvoiceB2BReportModel>>(b2bInvoices);
                //        dataEntity.Entity = invoiceB2BModel;
                //    }

                //    if (invoiceDetail.Count() == 1)
                //        fileName = reportSetting.PDFPrefix + invoiceDetail?.FirstOrDefault()?.InvoiceNo.ToString();
                //    else
                //        fileName = "invoices";
                //    break;
                //case JsReportPdfDocumentTypes.SO:
                //    recordId = Guid.Parse(id);
                //    var _orders = new List<OrderHeader>();
                //    var _order = _orderService.GetOrderDetail(recordId, false);
                //    var lineitmes = _order.OrderLines.Where(x => string.IsNullOrEmpty(x.ParentProductId) || x.ParentProductId == Guid.Empty.ToString()).ToList();
                //    foreach (var line in lineitmes)
                //    {
                //        line.ChildItems = _order.OrderLines.Where(x => x.ParentProductId.ToLower() == line.ProductId.ToString().ToLower()).Select(x => new ChildOrderLine { ProductName = x.ProductName, Qty = x.Qty, StockCode = x.StockCode, ShortDescription = x.ShortDescription }).ToList();
                //    }
                //    _order.OrderLines = lineitmes;
                //    _orders.Add(_order);
                //    dataEntity.Entity = _orders;
                //    fileName = reportSetting.PDFPrefix + _order?.OrderNo;
                //    break;
                //case JsReportPdfDocumentTypes.PK:
                //    var picklistData = _orderProcessingService.GetPicklistPdfData(_sessionContext.OrgId, id);
                //    dataEntity.Entity = picklistData;
                //    if (picklistData.Count == 1)
                //        fileName = reportSetting.PDFPrefix + picklistData?.FirstOrDefault()?.RefNumber.ToString();
                //    else
                //        fileName = "Picklists";
                //    break;
                //case JsReportPdfDocumentTypes.PKO:
                //    recordId = Guid.Parse(id);
                //    var _picklistOLs = new List<Picklist>();
                //    var _picklistOL = _orderProcessingService.GetPicklistDetail(recordId);
                //    foreach (var shipment in _picklistOL.OrderShipments)
                //    {
                //        var orderShipment = shipment.DeliveryNoteLines.Where(x => string.IsNullOrEmpty(x.ParentProductId) || x.ParentProductId == Guid.Empty.ToString()).ToList();
                //        foreach (var ship in orderShipment)
                //        {
                //            ship.ChildProds = shipment.DeliveryNoteLines.Where(x => x.ParentProductId.ToLower() == ship.RecordId.ToString().ToLower())
                //                                .Select(x => new ChildProductSummary { ProductName = x.ProductName, StockCode = x.StockCode, CustomInfo1Formatted = x.CustomInfo1Formatted })
                //                                .FirstOrDefault();
                //        }
                //        shipment.DeliveryNoteLines = orderShipment;
                //    }
                //    _picklistOLs.Add(_picklistOL);
                //    dataEntity.Entity = _picklistOLs;
                //    fileName = reportSetting.PDFPrefix + _picklistOL?.RefNumber;
                //    break;
                //case JsReportPdfDocumentTypes.STR:
                //    recordId = Guid.Parse(id);
                //    var strPDFModel = new List<StockTransferHeaderModel>();
                //    var result = _inventoryService.GetStockTransferDetail(recordId);
                //    var strHeaderModel = Mapper.Map<StockTransferHeaderModel>(result);
                //    foreach (var line in strHeaderModel.Lines)
                //    {
                //        strHeaderModel.LinesQtySum += line.Qty;
                //    }

                //    strPDFModel.Add(strHeaderModel);
                //    dataEntity.Entity = strPDFModel;
                //    fileName = reportSetting.PDFPrefix + strHeaderModel?.CustomNo;
                //    break;
                //case JsReportPdfDocumentTypes.PO:
                //    var purchaseOrder = _purchaseService.GetPurchaseOrderDataForReport(_sessionContext.OrgId, id);
                //    var poModel = Mapper.Map<List<PurchaseOrderReportModel>>(purchaseOrder);
                //    dataEntity.Entity = poModel;
                //    if (poModel.Count == 1)
                //        fileName = reportSetting.PDFPrefix + poModel.FirstOrDefault()?.CustomNo;
                //    else
                //        fileName = "PurchaseOrders";
                //    break;
                //case JsReportPdfDocumentTypes.GR:
                //    recordId = Guid.Parse(id);
                //    var grPDFModel = new List<GoodReceivedReportModel>();
                //    var _goodRecieve = _purchaseService.GetGoodReceivedDetail(recordId);
                //    var lineItems = _goodRecieve.GoodReceivedLine.Where(x => x.ReceivedQty > 0).ToList();
                //    _goodRecieve.GoodReceivedLine = lineItems;
                //    var grModel = Mapper.Map<GoodReceivedReportModel>(_goodRecieve);

                //    grPDFModel.Add(grModel);
                //    dataEntity.Entity = grPDFModel;
                //    fileName = reportSetting.PDFPrefix + grModel?.CustomNo;
                //    break;
                //case JsReportPdfDocumentTypes.SL:
                //    var labelData = _orderRoutingService.GetShippingLabelPDFData(_sessionContext.OrgId, id);
                //    dataEntity.Entity = labelData;
                //    if (labelData.Count == 1)
                //        fileName = reportSetting.PDFPrefix + labelData?.FirstOrDefault()?.TrackingNo.ToString();
                //    else
                //        fileName = "ShippingLabels";
                //    break;
                //case JsReportPdfDocumentTypes.PA:
                //    var putawayHeaderList = new List<PutawayPdfModel>();
                //    recordId = Guid.Parse(id);
                //    var putawayHeader = _inventoryService.GetPutawayListDetail(_sessionContext.OrgId, recordId);
                //    var putawayHeaderModel = Mapper.Map<PutawayPdfModel>(putawayHeader);
                //    putawayHeaderList.Add(putawayHeaderModel);
                //    dataEntity.Entity = putawayHeaderList;
                //    fileName = reportSetting.PDFPrefix + putawayHeaderModel?.CustomNo;
                //    break;
                //case JsReportPdfDocumentTypes.MF:
                //    var data = _carrierService.GetManifestPDFData(_sessionContext.OrgId, id);
                //    dataEntity.Entity = data;
                //    if (data.Count == 1)
                //        fileName = reportSetting.PDFPrefix + data?.FirstOrDefault()?.ManifestNo.ToString();
                //    else
                //        fileName = "Manifests";
                //    break;

                //case JsReportPdfDocumentTypes.LOC:
                //    var locations = new List<LocationForBarCodePrint>();
                //    if (TempData["locationforBarcCodePrint"] != null)
                //        locations = ((IEnumerable)TempData["locationforBarcCodePrint"]).Cast<LocationForBarCodePrint>().ToList();
                //    var locationData = _deliveryCenterService.GetLocationLabelPdfData(_sessionContext.OrgId, locations);
                //    dataEntity.Entity = locationData;
                //    if (locationData.Count == 1)
                //        fileName = reportSetting.PDFPrefix + locationData?.FirstOrDefault()?.LocationName.ToString();
                //    else
                //        fileName = "Locations";
                //    break;
                //case JsReportPdfDocumentTypes.PD:
                //    var stockCode = new List<ProductForBarCodePrint>();
                //    if (TempData["productforBarcCodePrint"] != null)
                //        stockCode = ((IEnumerable)TempData["productforBarcCodePrint"]).Cast<ProductForBarCodePrint>().ToList();
                //    var productdata = _productService.GetProductLabelPdfData(_sessionContext.OrgId, stockCode);
                //    dataEntity.Entity = productdata;
                //    if (productdata.Count == 1)
                //        fileName = reportSetting.PDFPrefix + productdata?.FirstOrDefault()?.StockCode.ToString();
                //    else
                //        fileName = "Products";
                //    break;
                //case JsReportPdfDocumentTypes.SRQ:
                //    recordId = Guid.Parse(id);
                //    var stockRequestPDFModel = new List<StockRequestReportModel>();
                //    var stockRequestDetail = _inventoryService.GetStockRequestDetail(_sessionContext.OrgId, recordId);
                //    var stockRequetsModel = Mapper.Map<StockRequestReportModel>(stockRequestDetail);
                //    stockRequestPDFModel.Add(stockRequetsModel);
                //    dataEntity.Entity = stockRequestPDFModel;
                //    fileName = reportSetting.PDFPrefix + stockRequetsModel?.CustomNo;
                //    break;
                //case JsReportPdfDocumentTypes.SR:
                //    recordId = Guid.Parse(id);
                //    var stockReceivePDFModel = new List<StockTransferReceivedReportModel>();
                //    var stockReceiveDetail = _inventoryService.GetStockTransferReceivedDetail(_sessionContext.OrgId, recordId);
                //    var stockReceiveModel = Mapper.Map<StockTransferReceivedReportModel>(stockReceiveDetail);
                //    stockReceivePDFModel.Add(stockReceiveModel);
                //    dataEntity.Entity = stockReceivePDFModel;
                //    fileName = reportSetting.PDFPrefix + stockReceiveModel?.ReceivingNo;
                //    break;
                //case JsReportPdfDocumentTypes.SN:
                //    recordId = Guid.Parse(id);
                //    var supDNotePdfHaeder = new List<SupplierDNoteReportModel>();
                //    var supplierDNote = _purchaseService.GetSupplierDNoteDetail(recordId);
                //    var supDNoteDetail = Mapper.Map<SupplierDNoteReportModel>(supplierDNote);
                //    supDNotePdfHaeder.Add(supDNoteDetail);
                //    dataEntity.Entity = supDNotePdfHaeder;
                //    fileName = reportSetting.PDFPrefix + supDNoteDetail?.DeliveryNoteNo;
                //    break;
                //case JsReportPdfDocumentTypes.JS:
                //    recordId = Guid.Parse(id);
                //    var sheet = _orderService.GetJobSheetDetail(recordId, _sessionContext.OrgId);
                //    dataEntity.Entity = sheet;
                //    fileName = reportSetting.PDFPrefix + sheet?.FirstOrDefault().OrderNo;
                //    break;
                case JsReportPdfDocumentTypes.TR:
                    var tray = new List<TrayForBarCodePrint>();
                    if (TempData["trayforBarcCodePrint"] != null)
                        tray = ((IEnumerable)TempData["trayforBarcCodePrint"]).Cast<TrayForBarCodePrint>().ToList();
                    var traydata = _trayRepository.GetTrayLabelPdfData(Guid.Parse("30231bed-c539-41f0-a369-c895da36566c"), tray);
                    dataEntity.Entity = traydata;
                    if (tray.Count == 1)
                        fileName ="TR" + tray?.FirstOrDefault()?.Code.ToString();
                        //fileName = reportSetting.PDFPrefix + tray?.FirstOrDefault()?.Code.ToString();
                    else
                        fileName = "Trays";
                    break;
                default:
                    // code block
                    break;
            }
            //for barcode :WZ917dZ, for code : zKxpKt6
            templateModel.Template = new Template { ShortId = "zKxpKt6", Recipe = "chrome-pdf", Engine = "handlebars" };
           // templateModel.Template = new Template { ShortId = reportSetting.ShortId, Recipe = reportSetting.Recipe, Engine = reportSetting.Engine };
            templateModel.Data = dataEntity;

            var responseDoc = _contentService.GetJsReportContent(templateModel);

            string mimeType = "application/pdf";
            Response.AppendHeader("Content-Disposition", "inline; filename=" + fileName + ".pdf");
            return File(responseDoc, mimeType);
        }
    }
}