using Microsoft.AspNetCore.Mvc;
using Crud.Service.ProductService;
using Crud.Data.Entities;
using System.Reflection;
using Crud.Api.Model;
using AutoMapper;
using System.Net;
using Crud.Api.Model.Product;

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
		public List<Product> GetAll(int currentPage ,int pageSize=40)
		{
			var productlist = _productService.GetProductList(currentPage, pageSize);
			return productlist;
		}

		// GET api/<ProductController>/5
		[HttpGet("{id}")]
		public ResponseModel<ProductDetailModel> Get(int id)
		{
			var response = new ResponseModel<ProductDetailModel>();
			try
			{
				var result = _productService.GetProductListById(id);
				var mappedProduct = _mapper.Map<ProductDetailModel>(result);
				// Prepare a successful response
				response.Status = "Success";
				response.StatusCode = (int)HttpStatusCode.OK; // Using HttpStatusCode
				response.Result = mappedProduct;
				//response.Message = result.Message;
			}
			catch (Exception ex)
			{
				// Prepare a failure response
				response.Status = "Error";
				response.StatusCode = (int)HttpStatusCode.InternalServerError; // Using HttpStatusCode
				response.Message = "An error occurred while saving the product.";
				response.ErrorDetails.Add(ex.Message);

			}
			return response;
		}

		// in here response is not visible in swagger ui
		//// POST api/<ProductController>
		//[HttpPost]
		//public ActionResult Post(ProductModel model)
		//{
		//	var response = new ReposeModel<BoolResponse>();
		//	try
		//	{
		//		// Map ProductModel to Product entity
		//		var mappedProduct = _mapper.Map<Product>(model);

		//		// Save the product using the service
		//		var result = _productService.SaveProduct(mappedProduct);

		//		// Prepare a successful response
		//		response.Status = "Success";
		//		response.StatusCode = (int)HttpStatusCode.OK; // Using HttpStatusCode
		//		response.Result = result;
		//		response.Message = result.Message;
		//		return Ok(response); // Return 200 OK with response data
		//	}
		//	catch (Exception ex)
		//	{
		//		// Prepare a failure response
		//		response.Status = "Error";
		//		response.StatusCode = (int)HttpStatusCode.InternalServerError; // Using HttpStatusCode
		//		response.Message = "An error occurred while saving the product.";
		//		response.ErrorDetails.Add(ex.Message);

		//		return StatusCode((int)HttpStatusCode.InternalServerError, response); // Return 500 Internal Server Error

		//	}
		//}
		// POST api/<ProductController>
		[HttpPost]
		public ResponseModel<BoolResponse> Post(ProductModel model)
		{
			var response = new ResponseModel<BoolResponse>();
			try {
				// Map ProductModel to Product entity
				var mappedProduct = _mapper.Map<Product>(model);

				// Save the product using the service
				var result = _productService.SaveProduct(mappedProduct);

				// Prepare a successful response
				response.Status = "Success";
				response.StatusCode = (int)HttpStatusCode.OK; // Using HttpStatusCode
				response.Result = result;
				response.Message = result.Message;
				
			}
			catch (Exception ex) {
				// Prepare a failure response
				response.Status = "Error";
				response.StatusCode = (int)HttpStatusCode.InternalServerError; // Using HttpStatusCode
				response.Message = "An error occurred while saving the product.";
				response.ErrorDetails.Add(ex.Message);

			}
			return response;
		}

		// PUT api/<ProductController>/5
		[HttpPut]
		public ResponseModel<BoolResponse> Put(UpdateProductModel model)
		{
			var response = new ResponseModel<BoolResponse>();
			try
			{
				// Map ProductModel to Product entity
				var mappedProduct = _mapper.Map<Product>(model);
				// Save the product using the service
				var result = _productService.UpsertProduct(mappedProduct);

				// Prepare a successful response
				response.Status = "Success";
				response.StatusCode = (int)HttpStatusCode.OK; // Using HttpStatusCode
				response.Result = result;
				response.Message = result.Message;
			}
			catch (Exception ex)
			{
				// Prepare a failure response
				response.Status = "Error";
				response.StatusCode = (int)HttpStatusCode.InternalServerError; // Using HttpStatusCode
				response.Message = "An error occurred while saving the product.";
				response.ErrorDetails.Add(ex.Message);

			}
			return response;
		}

		// DELETE api/<ProductController>/5
		[HttpDelete("{id}")]
		public ResponseModel<BoolResponse> Delete(int id)
		{


			var response = new ResponseModel<BoolResponse>();
			try
			{
				// Save the product using the service
				var result = _productService.DeleteProduct(id);

				// Prepare a successful response
				response.Status = "Success";
				response.StatusCode = (int)HttpStatusCode.OK; // Using HttpStatusCode
				response.Result = result;
				response.Message = result.Message;
			}
			catch (Exception ex)
			{
				// Prepare a failure response
				response.Status = "Error";
				response.StatusCode = (int)HttpStatusCode.InternalServerError; // Using HttpStatusCode
				response.Message = "An error occurred while saving the product.";
				response.ErrorDetails.Add(ex.Message);

			}
			return response;

			//var res = _productService.DeleteProduct(id);
			//return res;
		}
	}
}
