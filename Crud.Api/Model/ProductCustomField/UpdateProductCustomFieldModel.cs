namespace Crud.Api.Model.ProductCustomField
{
	public class UpdateProductCustomFieldModel
	{ 
	    public Guid Id { get; set; }
	    public string FieldCode { get; set; }
		public string FieldName { get; set; }
		public byte InputType { get; set; }
	}
}
