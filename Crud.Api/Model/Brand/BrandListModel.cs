namespace Crud.Api.Model.Brand
{
	public class BrandListModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string ShortDescription { get; set; }
		public string Description { get; set; }
		public DateTime LastUpdated { get; set; }
		public string LastUpdatedBy { get; set; }
	}
}
