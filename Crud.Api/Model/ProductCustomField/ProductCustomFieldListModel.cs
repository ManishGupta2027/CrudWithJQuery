namespace Crud.Api.Model.ProductCustomField
{
	public class ProductCustomFieldListModel
	{
		public Guid Id { get; set; }
		public string FieldCode { get; set; }
		public string FieldName { get; set; }
		public byte InputType { get; set; }
		public DateTime LastUpdated { get; set; }
		public string LastUpdatedBy { get; set; }
	}
}
