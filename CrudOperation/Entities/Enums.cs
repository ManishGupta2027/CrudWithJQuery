using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrudOperation.Entities
{
    public enum Enums
    {
    }
    public enum JsReportPdfDocumentTypes
    {
        None = 0,
        IN = 1, //Invoice
        SO = 2, //SalesOrder/OrderDetail
        PK = 3, //Picklist
        STR = 4, //StockTransfer
        GR = 5, //GRN
        PO = 6, //PO
        SRQ = 7, //StockRequest
        DN = 8, //DeliveryNote
        SN = 9, //SupplierDeliveryNote
        SR = 10, //StockTransferReceived
        PKO = 11, //Picklist OrderList
        SL = 12, //Shipping Label
        PA = 13, //PutawayList
        MF = 14, //Manifest
        LOC = 15,//Loaction
        INB = 16, // B2B Invoices
        PD = 17, // Product
        JS = 18, //Job Sheet
        TR = 19 //Tray
    }

}