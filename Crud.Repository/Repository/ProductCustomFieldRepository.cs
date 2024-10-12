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
using Newtonsoft.Json;
using Crud.Data.Entities.Category;
using Crud.Data.Entities.CustomAttributeSet;

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
			var json= JsonConvert.SerializeObject(productCustomField);
			DynamicParameters dbParams = new DynamicParameters();
			dbParams.AddDynamicParams(
				new
				{
					@Id = productCustomField.Id,
					@ProductCustomFieldJson= json
				}
			);
			var dbResponse = _dapperRepository.Update<BoolResponse>("procUpsertProductCustomField_20241002", dbParams, "MasterDataConnectionStrings");
			return dbResponse;
		}
		public ProductCustomField GetProductCustomFieldById(Guid id)
		{
			DynamicParameters dbParams = new DynamicParameters();
			dbParams.AddDynamicParams(new { @Id = id });

			// Execute the stored procedure
			var dbResponse = _dapperRepository.Get<dynamic>("procGetJsonProductCustomFieldDetail_20241003", dbParams, "MasterDataConnectionStrings");

			// Check if dbResponse is not null
			if (dbResponse != null)
			{
				// Access the JsonResult property that contains the JSON string
				var json = dbResponse.JsonResult;

				// Deserialize the JSON into the ProductCustomField object
				var data = JsonConvert.DeserializeObject<ProductCustomField>(json);

				return data;
			}

			return null; // Return null if no data is found
		}


		public BoolResponse UpsertProductCustomField(ProductCustomField productCustomField)
		{
			var json = JsonConvert.SerializeObject(productCustomField);
			DynamicParameters dbParams = new DynamicParameters();
			dbParams.AddDynamicParams(
				new
				{
					@Id = productCustomField.Id,
					@ProductCustomFieldJson = json
				});
			var dbResponse = _dapperRepository.Update<BoolResponse>("procUpsertProductCustomField_20241002", dbParams, "MasterDataConnectionstrings");
			return dbResponse;
		}

		public List<ProductCustomField> GetProductCustomFieldList(int currentPage, int pageSize)
		{
			DynamicParameters dbParams = new DynamicParameters();
			dbParams.AddDynamicParams(
				new
				{
					@CurrentPage = 1,
					@PageSize = 40

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

		//          CustomAttributeSet

		//public BoolResponse SaveCustomAttributeSet(CustomAttributeSet customAttributeSet)
		//{
		//	DynamicParameters dbParams = new DynamicParameters();
		//	dbParams.AddDynamicParams(
		//		new
		//		{
		//			@id = customAttributeSet.Id,
		//			@Name = customAttributeSet.SetName,
		//		}
		//	);
		//	var dbResponse = _dapperRepository.Update<BoolResponse>("procUpsertProductCustomFieldSet_20241005", dbParams, "MasterDataConnectionStrings");
		//	return dbResponse;
		//}
		public CustomAttributeSet GetCustomAttributeSetById(Guid id)
		{
			DynamicParameters dbParams = new DynamicParameters();
			dbParams.AddDynamicParams(
				new
				{
					@Id = id,
				}
			);
			var dbResponse = _dapperRepository.Get<CustomAttributeSet>("procGetProductCustomFieldSetDetail_20241006", dbParams, "MasterDataConnectionStrings");
			//var a  = new List<Product>();
			return dbResponse;
		}


		public BoolResponse UpsertCustomAttributeSet(CustomAttributeSet customAttributeSet)
		{
			DynamicParameters dbParams = new DynamicParameters();
			dbParams.AddDynamicParams(
				new
				{
					@id = customAttributeSet.Id,
					@Name = customAttributeSet.SetName,

				});
			var dbResponse = _dapperRepository.Update<BoolResponse>("procUpsertProductCustomFieldSet_20241005", dbParams, "MasterDataConnectionstrings");
			return dbResponse;
		}

		public List<CustomAttributeSet> GetCustomAttributeSetList(int currentPage, int pageSize, string setName)
		{
			DynamicParameters dbParams = new DynamicParameters();
			dbParams.AddDynamicParams(
				new
				{
					@CurrentPage = currentPage,
					@PageSize = pageSize,
					@Name = setName
				}
			);
			var dbResponse = _dapperRepository.GetAll<CustomAttributeSet>("procGetProductCustomFieldSetList_20241006", dbParams, "MasterDataConnectionStrings");
			return dbResponse;
		}
		public BoolResponse DeleteCustomAttributeSet(Guid id)
		{
			DynamicParameters dbParams = new DynamicParameters();
			dbParams.AddDynamicParams(new { @Id = id });
			var dbResponse = _dapperRepository.Update<BoolResponse>("procDeleteProductCustomFieldSet_20241006", dbParams, "MasterDataConnectionStrings");
			return dbResponse;
		}

	}
}
