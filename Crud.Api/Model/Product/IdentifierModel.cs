namespace Crud.Api.Model.Product
{
    public class IdentifierModel
    {
        public string SKU { get; set; }
        public string StockCode { get; set; }
        public string EAN { get; set; }  // European Article Number
        public string UPC { get; set; }  // Universal Product Code
    }
}
