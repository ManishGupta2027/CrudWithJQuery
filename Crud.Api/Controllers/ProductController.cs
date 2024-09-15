using Microsoft.AspNetCore.Mvc;
using Crud.Service.ProductService;
using Crud.Data.Entities;
using System.Reflection;
using Crud.Api.Model;
using AutoMapper;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Crud.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private IProductService _productService;
		private readonly IMapper _mapper;
		public ProductController(IProductService productService, IMapper mapper)
        {
			_mapper = mapper;
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

	// POST api/<ProductController>
		[HttpPost]
		public bool Post(ProductModel model)
		{
			var mapped = _mapper.Map<Product>(model);
			var res = _productService.SaveProduct(mapped);
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
