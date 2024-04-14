﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrudOperation.Models;

namespace CrudOperation.Repository
{
	public interface IProductRepository
	{
		bool SaveProduct(Product product);
		List<Product> GetProductList();
	}
}
