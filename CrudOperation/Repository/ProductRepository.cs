using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CrudOperation.Data;
using CrudOperation.Models;
using CrudOperation.Data;

namespace CrudOperation.Repository
{
	public class ProductRepository : IProductRepository
	{
		private readonly IDataFactory _dataFactory;
		private readonly DataFactoryDBDataContext _dataFactoryDBDataContext;
		public ProductRepository(IDataFactory dataFactory, DataFactoryDBDataContext dataFactoryDBDataContext)
		{
			_dataFactory = dataFactory;
			_dataFactoryDBDataContext = _dataFactory.DataFactoryDBDataContext();
		}

	

		public List<Product> GetProductList()
		{
			var dbResp = _dataFactoryDBDataContext.procGetProductList_14042024();
			var Product = (from o in dbResp
						select new Product
						{
							Title = o.Title,
							StockCode = o.StockCode,
							Price = (decimal)o.Price,
							Category = o.Category,
							Id = o.id,
						}).ToList();
			return Product;

		}

		public Product GetProductListById(int id)
		{
			var dbResp = _dataFactoryDBDataContext.procGetProductDetail_14042024(id);
			var product = (from o in dbResp
						   select new Product
						   {
							   Title = o.Title,
							   StockCode = o.StockCode,
							   Price = (decimal)o.Price,
							   Category = o.Category,
							   Id = o.id,
						   }).FirstOrDefault();
			return product;
		}

		public bool UpsertProduct(Product product)
		{
			// Check if the new StockCode is the same as the existing StockCode
			var existingProduct = GetProductListById(product.Id);
			if (existingProduct == null)
			{
				return false;
			}

			if (existingProduct.StockCode != product.StockCode)
			{
				return false;
			}

			// Proceed with the update operation
			var res = _dataFactoryDBDataContext.procUpsertProduct_14042024(product.Id,product.Title, product.StockCode, product.Price,product.Category);
			var isValid = res.FirstOrDefault()?.isValid;
			return isValid ?? false;
		}
		public bool DeleteProduct(int id)
		{
			// Implement logic to delete user from the database
			var dbResp = _dataFactoryDBDataContext.procDeleteProduct_15042024(id);
			var isValid = dbResp.FirstOrDefault()?.isValid;
			return isValid ?? false;
		}
		public bool SaveProduct(Product product)
		{
			var res = _dataFactoryDBDataContext.procSaveProduct_11042024(product.Title, product.StockCode, product.Price, product.Category);
		var isValid=res.FirstOrDefault().isValid;
			return (bool)isValid;
		}
	}
}