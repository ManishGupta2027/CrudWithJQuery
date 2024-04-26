using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrudOperation.Models;

namespace CrudOperation.Service
{
	public interface IProductService
	{
		bool SaveProduct(Product product);
		List<Product> GetProductList(int currentPage, int pageSize);
		Response UpsertProduct(Product product);
		Product GetProductListById(int id);
		bool DeleteProduct(int id);
	}
}
