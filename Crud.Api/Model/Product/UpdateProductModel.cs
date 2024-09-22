namespace Crud.Api.Model.Product
{
    public class UpdateProductModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string StockCode { get; set; }
        public decimal Price { get; set; }
		public Guid? CategoryId { get; set; }
		public Guid? BrandId { get; set; }
		public string Gender { get; set; }
        public bool IsActive { get; set; }
    }
}
