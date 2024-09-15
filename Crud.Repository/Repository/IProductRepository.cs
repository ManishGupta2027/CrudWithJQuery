using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crud.Data.Entities;

namespace Crud.Data.Repository
{
	public interface IProductRepository
	{
		BoolResponse SaveProduct(Product product);
		List<Product> GetProductList(int currentPage, int pageSize);
		BoolResponse UpsertProduct(Product product);
		Product GetProductListById(int id);
		BoolResponse DeleteProduct(int id);
	}
}
