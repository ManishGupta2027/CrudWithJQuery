using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crud.Data.Entities.Brand;
using Crud.Data.Entities;
using Crud.Data.Entities.ProductCustomField;

namespace Crud.Data.Repository
{
	public interface IProductCustomFieldRepository
	{
		BoolResponse SaveProductCustomField(ProductCustomField productCustomField);
		ProductCustomField GetProductCustomFieldById(Guid id);
		BoolResponse UpsertProductCustomField(ProductCustomField productCustomField);
		List<ProductCustomField> GetProductCustomFieldList(int currentPage, int pageSize);
		BoolResponse DeleteProductCustomField(Guid id);
	}
}
