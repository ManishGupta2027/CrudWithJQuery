using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crud.Data.Entities;
using Crud.Data.Entities.Product;
using Crud.Data.Enums;

namespace Crud.Data.Repository
{
    public interface IProductRepository
	{
		BoolResponse SaveProduct(Product product);
		List<Product> GetProductList(int currentPage, int pageSize);
		BoolResponse UpdateProduct(Guid id, UpdateProduct product);
		UpdateProduct GetProductById(Guid id);
		BoolResponse DeleteProduct(Guid id);
		BoolResponse UpdateProductStatus(Guid id, ProductStatus status);
	}
}
