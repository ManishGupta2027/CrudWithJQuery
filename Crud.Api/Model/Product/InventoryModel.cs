namespace Crud.Api.Model.Product
{
    public class InventoryModel
    {
        public int StockQuantity { get; set; }
        public int MinOrderQuantity { get; set; }
        public int MaxOrderQuantity { get; set; }
        public decimal Weight { get; set; }
        public string Dimensions { get; set; }
    }
}
