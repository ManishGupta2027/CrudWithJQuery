using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crud.Data.Entities;
using Crud.Data.Entities.Product;

namespace Crud.Data.Repository
{
    public interface IProductRepository
	{
		BoolResponse SaveProduct(Product product);
		List<Product> GetProductList(int currentPage, int pageSize);
		BoolResponse UpsertProduct(Product product);
		Product GetProductById(Guid id);
		BoolResponse DeleteProduct(Guid id);
	}
}
