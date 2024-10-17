using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud.Data.Entities.ProductCustomField
{
	public class ProductCustomField : BaseEntity
	{
		public string FieldCode { get; set; }
		public string FieldName { get; set; }
		public int InputType { get; set; }
		public List<CustomFieldOption>? Options { get; set; }

	}
	public class CustomFieldOption
	{ 
		public string OptionText { get; set; }
		public string OptionValue { get; set; }
		public bool IsDefault { get; set; }
		public int DisplayOrder { get; set; }
	}
}
