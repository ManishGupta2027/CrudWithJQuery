using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crud.Data.Entities;

namespace Crud.Service.ProductService
{
	public interface IProductService
	{
		BoolResponse SaveProduct(Product product);
		List<Product> GetProductList(int currentPage, int pageSize);
		Response UpsertProduct(Product product);
		Product GetProductListById(int id);
		bool DeleteProduct(int id);
	}
}
