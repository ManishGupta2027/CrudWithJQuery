using Microsoft.AspNetCore.Mvc;
using Crud.Service.ProductService;
using Crud.Data.Entities;
using System.Reflection;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Crud.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private IProductService _productService;
		public ProductController(IProductService productService)
        {
			_productService = productService;

		}
        // GET: api/<ProductController>
        [HttpGet]
		public List<Product> GetAll()
		{
			var productlist = _productService.GetProductList(1, 30);
			return productlist;
		}

		// GET api/<ProductController>/5
		[HttpGet("{id}")]
		public Product Get(int id)
		{
			var productDetail=_productService.GetProductListById(id);
			return productDetail;
		}

	//	var user = _productService.GetProductListById(id);
	//		if (user == null)
	//		{
	//			return HttpNotFound(); // Or some other appropriate action
	//}
	//		return View(user);

	// POST api/<ProductController>
	[HttpPost]
		public bool Post(Product model)
		{
			var res = _productService.SaveProduct(model);
		return res;
		}

		// PUT api/<ProductController>/5
		[HttpPut]
		public Response Put(Product model)
		{
			var res = _productService.UpsertProduct(model);
			return res;
		}

		// DELETE api/<ProductController>/5
		[HttpDelete("{id}")]
		public bool Delete(int id)
		{
			var res = _productService.DeleteProduct(id);
			return res;
		}
	}
}
