using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crud.Data.Entities;
using Crud.Data.Entities.Product;
using Crud.Data.Enums;

namespace Crud.Service.ProductService
{
    public interface IProductService
	{
		BoolResponse SaveProduct(Product product);
		List<Product> GetProductList(int currentPage, int pageSize, string name=null);
		BoolResponse UpdateProduct(Guid id, UpdateProduct product);
		UpdateProduct GetProductById(Guid id);
		BoolResponse UpdateProductStatus(Guid productId, ProductStatus status);
		BoolResponse DeleteProduct(Guid id);
	}
}
