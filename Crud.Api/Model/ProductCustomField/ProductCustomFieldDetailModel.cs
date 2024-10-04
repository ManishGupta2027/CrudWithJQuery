﻿namespace Crud.Api.Model.ProductCustomField
{
	public class ProductCustomFieldDetailModel : ProductCustomFieldModel
	{
		public Guid Id { get; set; }
		public List<CustomFieldOptionModel>? Options { get; set; }
	}
}
