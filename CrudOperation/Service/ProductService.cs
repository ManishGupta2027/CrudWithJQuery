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
		public bool SaveProduct(Product product)
		{
			var res = _productRepository.SaveProduct(product);
			return res;
		}
	}
}