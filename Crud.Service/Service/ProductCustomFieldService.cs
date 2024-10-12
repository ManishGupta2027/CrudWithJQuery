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
using Crud.Data.Entities.CustomAttributeSet;

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

		//                    CustomAttributeSet

		//public BoolResponse SaveCustomAttributeSet(CustomAttributeSet customAttributeSet)
		//{
		//	var res = _productCustomFieldRepository.SaveCustomAttributeSet(customAttributeSet);
		//	return res;
		//}
		public BoolResponse UpsertCustomAttributeSet(CustomAttributeSet customAttributeSet)
		{
			return _productCustomFieldRepository.UpsertCustomAttributeSet(customAttributeSet);
		}
		public List<CustomAttributeSet> GetCustomAttributeSetList(int currentPage, int pageSize)
		{
			return _productCustomFieldRepository.GetCustomAttributeSetList(currentPage, pageSize);
		}
		public CustomAttributeSet GetCustomAttributeSetById(Guid id)
		{
			return _productCustomFieldRepository.GetCustomAttributeSetById(id);
		}
		public BoolResponse DeleteCustomAttributeSet(Guid id)
		{
			return _productCustomFieldRepository.DeleteCustomAttributeSet(id);
		}
	}
}
