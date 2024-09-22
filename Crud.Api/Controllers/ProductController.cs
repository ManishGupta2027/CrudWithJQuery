using Microsoft.AspNetCore.Mvc;
using Crud.Service.ProductService;
using Crud.Data.Entities;
using System.Reflection;
using Crud.Api.Model;
using AutoMapper;
using System.Net;
using Crud.Api.Model.Product;
using Crud.Data.Entities.Product;

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
		public ResponsecPaginationModel<List<ProductListModel>> GetAll(int currentPage ,int pageSize=40)
		{
			var res = new ResponsecPaginationModel<List<ProductListModel>>();
			var productlist = _productService.GetProductList(currentPage, pageSize);
			var mappedProductList = _mapper.Map<List<ProductListModel>>(productlist);
			// Populate the response model
			res.Status = "Success";
			res.StatusCode = 200;
			res.Result = mappedProductList;
			res.CurrentPage = currentPage;
			res.PageSize = pageSize;
			res.TotalRecords = productlist[0].TotalRecords ?? 0;
			res.Message = "Products fetched successfully";
			return res;
		}

		// GET api/<ProductController>/5
		[HttpGet("{id}")]
		public ResponseModel<ProductDetailModel> Get(Guid id)
		{
			var response = new ResponseModel<ProductDetailModel>();
			try
			{
				var result = _productService.GetProductById(id);
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

		/// <summary>
		/// create product
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPost]
		public ResponseModel<BoolResponse> Post(ProductCreateModel model)
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
		public ResponseModel<BoolResponse> Delete(Guid id)
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
		}
	}
}
