using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crud.Data.Dapper;
using Crud.Data.Entities.Brand;
using Crud.Data.Entities;
using Dapper;
using Crud.Data.Entities.ProductCustomField;

namespace Crud.Data.Repository
{
	public class ProductCustomFieldRepository : IProductCustomFieldRepository
	{
		private readonly IDapperRepository _dapperRepository;
		public ProductCustomFieldRepository(IDapperRepository dapperRepository)
		{
			_dapperRepository = dapperRepository;

		}
		public BoolResponse SaveProductCustomField(ProductCustomField productCustomField)
		{
			DynamicParameters dbParams = new DynamicParameters();
			dbParams.AddDynamicParams(
				new
				{
					@Id = productCustomField.Id,
					@fieldCode = productCustomField.FieldCode,
					@fieldName = productCustomField.FieldName,
					@inputType = productCustomField.InputType,
				}
			);
			var dbResponse = _dapperRepository.Update<BoolResponse>("procUpsertProductCustomField_20240927", dbParams, "MasterDataConnectionStrings");
			return dbResponse;
		}

		public ProductCustomField GetProductCustomFieldById(Guid id)
		{
			DynamicParameters dbParams = new DynamicParameters();
			dbParams.AddDynamicParams(
				new
				{
					@Id = id,


				}
			);
			var dbResponse = _dapperRepository.Get<ProductCustomField>("procGetProductCustomFieldDetail_20240928", dbParams, "MasterDataConnectionStrings");
			//var a  = new List<Product>();
			return dbResponse;
		}

		public BoolResponse UpsertProductCustomField(ProductCustomField productCustomField)
		{
			DynamicParameters dbParams = new DynamicParameters();
			dbParams.AddDynamicParams(
				new
				{
					@id = productCustomField.Id,
					@fieldCode = productCustomField.FieldCode,
					@fieldName = productCustomField.FieldName,
					@inputType = productCustomField.InputType,
				});
			var dbResponse = _dapperRepository.Update<BoolResponse>("procUpsertProductCustomField_20240927", dbParams, "MasterDataConnectionstrings");
			return dbResponse;
		}

		public List<ProductCustomField> GetProductCustomFieldList(int currentPage, int pageSize)
		{
			DynamicParameters dbParams = new DynamicParameters();
			dbParams.AddDynamicParams(
				new
				{
					@CurrentPage = currentPage,
					@PageSize = pageSize

				}
			);
			var dbResponse = _dapperRepository.GetAll<ProductCustomField>("procGetProductCustomFieldList_20240928", dbParams, "MasterDataConnectionStrings");
			return dbResponse;
		}
		public BoolResponse DeleteProductCustomField(Guid id)
		{
			DynamicParameters dbParams = new DynamicParameters();
			dbParams.AddDynamicParams(new { @Id = id });
			var dbResponse = _dapperRepository.Update<BoolResponse>("procDeleteProductCustomField_20240928", dbParams, "MasterDataConnectionStrings");
			return dbResponse;
		}
	}
}
