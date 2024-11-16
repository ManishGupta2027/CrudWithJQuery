using Crud.Data.Entities;
using Crud.Data.Entities.Product;

namespace Crud.Service.ProductService
{
    public interface IProductService
	{
		BoolResponse SaveProduct(Product product);
		List<Product> GetProductList(int currentPage, int pageSize, string name=null);
		BoolResponse UpdateProduct(Guid id, UpdateProduct product);
		UpdateProduct GetProductById(Guid id);
		BoolResponse UpdateProductStatus(Guid productId, UpdateProductStatusModel model);
		BoolResponse DeleteProduct(Guid id);
	}
}
