namespace Crud.Api.Model.ProductCustomField
{
	public class UpdateProductCustomFieldModel
	{ 
	    public Guid Id { get; set; }
	    public string FieldCode { get; set; }
		public string FieldName { get; set; }
		public byte InputType { get; set; }
		public List<CustomFieldOptionModel>? Options { get; set; }
	}
	public class CustomFieldOptionModel
	{
		public string OptionText { get; set; }
		public string OptionValue { get; set; }
		public bool IsDefault { get; set; }
		public int DisplayOrder { get; set; }
	}
}
