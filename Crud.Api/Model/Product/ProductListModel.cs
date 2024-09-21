namespace Crud.Api.Model.Product
{
	public class ProductListModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string StockCode { get; set; }
		public decimal Price { get; set; }
		public string CategoryName { get; set; }
		public bool IsActive { get; set; }
	}
}
