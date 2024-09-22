using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crud.Data.Entities;
using Crud.Data.Dapper;
using Dapper;
using System.Diagnostics;
using System.Reflection;
using Crud.Data.Entities.Product;

namespace Crud.Data.Repository
{
    public class ProductRepository : IProductRepository
	{
		private readonly IDapperRepository _dapperRepository;
		public ProductRepository(IDapperRepository dapperRepository)
		{
			_dapperRepository = dapperRepository;
		}



		public List<Product> GetProductList(int currentPage, int pageSize)
		{
			DynamicParameters dbParams = new DynamicParameters();
			dbParams.AddDynamicParams(
				new
				{
					@CurrentPage = currentPage,
					@PageSize = pageSize

				}
			);
			var dbResponse = _dapperRepository.GetAll<Product>("procGetProductList_20240427", dbParams, "MasterDataConnectionStrings");
			return dbResponse;

		}
		public Product GetProductById(Guid id)
		{
			DynamicParameters dbParams = new DynamicParameters();
			dbParams.AddDynamicParams(
				new
				{
					@Id = id,
					

				}
			);
			var dbResponse = _dapperRepository.Get<Product>("procGetProductDetail_20240420", dbParams, "MasterDataConnectionStrings");
			return dbResponse;
		}
		public BoolResponse SaveProduct(Product product)
		{
			DynamicParameters dbParams = new DynamicParameters();
			dbParams.AddDynamicParams(
				new
				{
					@Id = product.Id,
					@Name = product.Name,
					@StockCode = product.StockCode,
					@Price = product.Price,
					@CategoryId = product.CategoryId,
					@BrandId = product.BrandId,
					@Gender = product.Gender,
					@IsActive = product.IsActive
				}
			);
			var dbResponse = _dapperRepository.Update<BoolResponse>("procUpsertProduct_20240427", dbParams, "MasterDataConnectionStrings");
			return dbResponse;

		}

		public BoolResponse UpsertProduct(Product product)
		{

			// Prepare the parameters for the upsert (Insert/Update)
			DynamicParameters dbParams = new DynamicParameters();
			dbParams.AddDynamicParams(new
			{
				@Id = product.Id,  // Null for insert, populated for update
				@Name = product.Name,
				@StockCode = product.StockCode,
				@Price = product.Price,
				@CategoryId = product.CategoryId,
				@BrandId = product.BrandId,
				@Gender = product.Gender,
				@IsActive = product.IsActive
			});

			// Execute the stored procedure using Dapper
			var dbResponse = _dapperRepository.Update<BoolResponse>("procUpsertProduct_20240427", dbParams, "MasterDataConnectionStrings");
			return dbResponse;
		}

		public BoolResponse DeleteProduct(Guid id)
		{
			DynamicParameters dbParams = new DynamicParameters();
			dbParams.AddDynamicParams(new { @Id = id });
			var dbResponse = _dapperRepository.Update<BoolResponse>("procDeleteProduct_15042024", dbParams, "MasterDataConnectionStrings");
			return dbResponse;
		}
	}
}
