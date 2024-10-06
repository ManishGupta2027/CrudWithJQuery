using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crud.Data.Entities.Brand;
using Crud.Data.Entities;
using Crud.Data.Entities.ProductCustomField;

namespace Crud.Service.ProductCustomFieldService
{
	public interface IProductCustomFieldService
	{
		BoolResponse SaveProductCustomField(ProductCustomField productCustomField);
		BoolResponse UpsertProductCustomField(ProductCustomField productCustomField);
		ProductCustomField GetProductCustomFieldById(Guid id);
		List<ProductCustomField> GetProductCustomFieldList(int currentPage, int pageSize);
		BoolResponse DeleteProductCustomField(Guid id);

		// CustomAttributeSet
		BoolResponse SaveCustomAttributeSet(CustomAttributeSet customAttributeSet);
		BoolResponse UpsertCustomAttributeSet(CustomAttributeSet customAttributeSet);
		CustomAttributeSet GetCustomAttributeSetById(Guid id);
		List<CustomAttributeSet> GetCustomAttributeSetList(int currentPage, int pageSize);
		BoolResponse DeleteCustomAttributeSet(Guid id);
	}
}
