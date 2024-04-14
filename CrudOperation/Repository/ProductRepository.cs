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
							id = o.id,
						}).ToList();
			return Product;

		}

		public bool SaveProduct(Product product)
		{
			var res = _dataFactoryDBDataContext.procSaveProduct_11042024(product.Title, product.StockCode, product.Price, product.Category);
		var isValid=res.FirstOrDefault().isValid;
			return (bool)isValid;
		}
	}
}