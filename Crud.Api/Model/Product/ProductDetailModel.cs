namespace Crud.Api.Model.Product
{
    public class ProductDetailModel 
    {
        public Guid Id { get; set; }
		public string Name { get; set; }
		public string StockCode { get; set; }
		public decimal Price { get; set; }
		public string CategoryName { get; set; }
		public string BrandName { get; set; }
		public string Gender { get; set; }
		public bool IsActive { get; set; }
		public bool IsVisible { get; set; }

	}
}
