namespace Crud.Api.Model.Product
{
    public class UpdateProductModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string StockCode { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public string Gender { get; set; }
        public bool IsActive { get; set; }
    }
}
