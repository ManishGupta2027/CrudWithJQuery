using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CrudOperation.Data;
using CrudOperation.Models;

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

		public bool SaveProduct(Product product)
		{
			throw new NotImplementedException();
		}

		public bool SaveUser(Product product)
		{
			var res = _dataFactoryDBDataContext.procSaveProduct_11042024(product.Title, product.StockCode, product.Price, product.Category);
		var isValid=res.FirstOrDefault().isValid;
			return (bool)isValid;
		}
	}
}