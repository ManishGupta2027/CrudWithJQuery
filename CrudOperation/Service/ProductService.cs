﻿using System;
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

		public Product GetProductListById(int id)
		{
			return _productRepository.GetProductListById(id);
		}
		public bool UpsertProduct(Product product)
		{
			return _productRepository.UpsertProduct(product);
		}
		public bool DeleteProduct(int id)
		{
			return _productRepository.DeleteProduct(id);
		}
		public bool SaveProduct(Product product)
		{
			var res = _productRepository.SaveProduct(product);
			return res;
		}
	}
}