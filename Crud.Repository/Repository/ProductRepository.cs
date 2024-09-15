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
			//var dbResp = _dataFactoryDBDataContext.procGetProductList_20240427(currentPage, pageSize);
			//var Product = (from o in dbResp
			//			   select new Product
			//			   {
			//				   Title = o.Title,
			//				   StockCode = o.StockCode,
			//				   Price = (decimal)o.Price,
			//				   Category = o.Category,
			//				   Id = o.id,
			//				   Gender = o.Gender,
			//				   IsActive = o.IsActive.HasValue ? o.IsActive.Value : false, // Convert bit to bool
			//				   TotalRecords = (int)o.TotalRecords,
			//				   CurrentPage = currentPage,
			//				   PageSize = pageSize,
			//				   CreatedBy = o.CreatedBy
			//			   }).ToList();
			//return Product;


			DynamicParameters dbParams = new DynamicParameters();
			dbParams.AddDynamicParams(
				new
				{
					@CurrentPage =1,
					@PageSize = 40
				
				}
			);
			var dbResponse = _dapperRepository.GetAll<Product>("procGetProductList_20240427", dbParams, "MasterDataConnectionStrings");
			//var a  = new List<Product>();
			return dbResponse;

		}
		public Product GetProductListById(int id)
		{
			//var dbResp = _dataFactoryDBDataContext.procGetProductDetail_20240420(id);
			//var product = (from o in dbResp
			//			   select new Product
			//			   {
			//				   Title = o.Title,
			//				   StockCode = o.StockCode,
			//				   Price = (decimal)o.Price,
			//				   Category = o.Category,
			//				   Id = o.id,
			//				   Gender = o.Gender,
			//				   IsActive = (bool)(o.IsActive ?? false), // Handling null value for IsActive field
			//			   }).FirstOrDefault();
			//return product;
			DynamicParameters dbParams = new DynamicParameters();
			dbParams.AddDynamicParams(
				new
				{
					@Id = id,
					

				}
			);
			var dbResponse = _dapperRepository.Get<Product>("procGetProductDetail_20240420", dbParams, "MasterDataConnectionStrings");
			//var a  = new List<Product>();
			return dbResponse;
		}

		//	DynamicParameters dbParams = new DynamicParameters();
		//	dbParams.AddDynamicParams(
		//			new
		//			{
		//				@Title = product.Title,
		//				@StockCode = product.StockCode,
		//				@Price =product.Price,
		//				@Category =product.Category,
		//				@Gender =product.Gender,
		//				@IsActive =product.IsActive
		//}
		//		);
		//		var dbResponse = _dapperRepository.Update<dynamic>("procSaveProduct_19042024", dbParams, "MasterDataConnectionStrings");
		//		return dbResponse.isValid;

		//public Response UpsertProduct(Product product)
		//{
		//	DynamicParameters dbParams = new DynamicParameters();
		//	dbParams.AddDynamicParams(
		//		new
		//		{
		//			@Product = product,


		//		}
		//	);
		//	var dbResponse = _dapperRepository.Get<Product>("procUpsertProduct_20240427", dbParams, "MasterDataConnectionStrings");
		//	//var a  = new List<Product>();
		//	return dbResponse;
		//}



		public BoolResponse UpsertProduct(Product product)
		{
			var response = new Response();

			// Prepare the parameters for the upsert (Insert/Update)
			DynamicParameters dbParams = new DynamicParameters();
			dbParams.AddDynamicParams(new
			{
				@Id = product.Id,  // Null for insert, populated for update
				@Title = product.Title,
				@StockCode = product.StockCode,
				@Price = product.Price,
				@Category = product.Category,
				@Gender = product.Gender,
				@IsActive = product.IsActive
			});

			// Execute the stored procedure using Dapper
			var dbResponse = _dapperRepository.Update<BoolResponse>("procUpsertProduct_20240427", dbParams, "MasterDataConnectionStrings");

			// Set response based on the result from the stored procedure
			//response.IsSuccess = dbResponse.isValid;
			//response.Message = dbResponse.Message;

			return dbResponse;
		}



		//	var res = _dataFactoryDBDataContext.procUpsertProduct_20240427(product.Id, product.Title, product.StockCode, product.Price, product.Category, product.Gender, product.IsActive);
		//	a = (from o in res
		//		 select new Response
		//		 {
		//			 isValid = o.isValid,
		//			 Message = o.Message,
		//		 }).FirstOrDefault();

		//	//var isValid = res.FirstOrDefault()?.isValid;
		//	return a;
		//}


		//public bool DeleteProduct(int id)
		//{
		//	// Implement logic to delete user from the database
		//	var dbResp = _dataFactoryDBDataContext.procDeleteProduct_15042024(id);
		//	var isValid = dbResp.FirstOrDefault()?.isValid;
		//	return isValid ?? false;
		//}

		public BoolResponse DeleteProduct(int id)
		{
			DynamicParameters dbParams = new DynamicParameters();
			dbParams.AddDynamicParams(new { @Id = id });
			var dbResponse = _dapperRepository.Update<BoolResponse>("procDeleteProduct_15042024", dbParams, "MasterDataConnectionStrings");
			return dbResponse;
		}
		

		public BoolResponse SaveProduct(Product product)
		{
			//var res = _dataFactoryDBDataContext.procSaveProduct_19042024(product.Title, product.StockCode, product.Price, product.Category, product.Gender, product.IsActive);
			//var isValid = res.FirstOrDefault().isValid;
			//return (bool)isValid;

			DynamicParameters dbParams = new DynamicParameters();
			dbParams.AddDynamicParams(
				new
				{
					@Id = product.Id,
					@Title = product.Title,
					@StockCode = product.StockCode,
					@Price =product.Price,
					@Category =product.Category,
					@Gender =product.Gender,
					@IsActive =product.IsActive
				}
			);
			var dbResponse = _dapperRepository.Update<BoolResponse>("procUpsertProduct_20240427", dbParams, "MasterDataConnectionStrings");
			//var res = new {
			//	dbResponse.isValid,
			//	dbResponse.isValid
			//};

			return dbResponse;

		}
	}
}
