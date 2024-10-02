using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crud.Data.Entities.Brand;
using Crud.Data.Entities;
using Crud.Data.Repository;
using Crud.Data.Entities.ProductCustomField;
using Crud.Service.ProductCustomFieldService;

namespace Crud.Service.ProductCustomfieldService
{
	public class ProductCustomFieldService : IProductCustomFieldService
	{
		private readonly IProductCustomFieldRepository _productCustomFieldRepository;
		public ProductCustomFieldService(IProductCustomFieldRepository productCustomFieldRepository)
		{
			_productCustomFieldRepository = productCustomFieldRepository;

		}
		public BoolResponse SaveProductCustomField(ProductCustomField productCustomField)
		{
			var res = _productCustomFieldRepository.SaveProductCustomField(productCustomField);
			return res;
		}
		public BoolResponse UpsertProductCustomField(ProductCustomField productCustomField)
		{
			return _productCustomFieldRepository.UpsertProductCustomField(productCustomField);
		}
		public List<ProductCustomField> GetProductCustomFieldList(int currentPage, int pageSize)
		{
			return _productCustomFieldRepository.GetProductCustomFieldList(currentPage, pageSize);
		}
		public ProductCustomField GetProductCustomFieldById(Guid id)
		{
			return _productCustomFieldRepository.GetProductCustomFieldById(id);
		}
		public BoolResponse DeleteProductCustomField(Guid id)
		{
			return _productCustomFieldRepository.DeleteProductCustomField(id);
		}
	}
}
