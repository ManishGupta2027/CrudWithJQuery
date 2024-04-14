using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CrudOperation.Models;
using CrudOperation.Repository;

namespace CrudOperation.Service
{
	public class ProductService:IProductService
	{
		private readonly IProductRepository _productRepository;
		public ProductService(IProductRepository productRepository)
		{
			_productRepository = productRepository;
		}

		public List<Product> GetProductList()
		{
			var res = _productRepository.GetProductList();
			return res;
		}

		public bool SaveProduct(Product product)
		{
			var res = _productRepository.SaveProduct(product);
			return res;
		}
	}
}