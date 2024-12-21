using Microsoft.AspNetCore.Mvc;
using Crud.Service.ProductService;
using Crud.Data.Entities;
using System.Reflection;
using Crud.Api.Model;
using AutoMapper;
using System.Net;
using Crud.Api.Model.Product;
using Crud.Data.Entities.Product;
using Crud.Service.Service.asset;
using Crud.Data.Entities.Brand;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Crud.Api.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
        readonly ILogger<ProductController> _logger;
        private IProductService _productService;
		private readonly IMapper _mapper;
        private readonly CloudinaryService _cloudinaryService;
        public ProductController(ILogger<ProductController> logger,IProductService productService, IMapper mapper, CloudinaryService cloudinaryService)
        {
			_mapper = mapper;
			_productService = productService;
			_cloudinaryService = cloudinaryService;
			_logger = logger;
        }
        // GET: api/<ProductController>
        [HttpGet]
		public ResponsecPaginationModel<List<ProductListModel>> GetAll(int currentPage =1,int pageSize=40, string name=null)
		{
            _logger.LogInformation("Information level log");
            var res = new ResponsecPaginationModel<List<ProductListModel>>();
			var productlist = _productService.GetProductList(currentPage, pageSize,name);
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
            _logger.LogInformation("Get product id:{@id}");
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
		public ResponseModel<BoolResponse> Create(ProductCreateModel model)
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
		[HttpPut("{id}")]
		public ResponseModel<BoolResponse> Update(Guid id,UpdateProductModel model)
		{
			var response = new ResponseModel<BoolResponse>();
			try
			{
				var prefixedFolder = "Product";
				if (model?.Media?.Files != null && model.Media.Files.Any())
				{
					
                    foreach (var item in model?.Media?.Files)
                    {
                        if (!string.IsNullOrEmpty(item.Base64))
                        {
                            // Name like abc.jpg than abc
                            var splitFileName = item.Name.Split('.');
                            string name = splitFileName[0];
                            item.Url = _cloudinaryService.UploadImage(item.Base64, $"{prefixedFolder}/{name}", "Store");
                        }

                    }
                }
            
                // Map ProductModel to Product entity
                var mappedProduct = _mapper.Map<UpdateProduct>(model);
				// Save the product using the service
				var result = _productService.UpdateProduct(id,mappedProduct);

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

		[HttpPut("{id}/status")]
		//public IActionResult UpdateProductStatus(Guid id, [FromBody] UpdateProductStatusModel model)
		//{
		//	if (model == null)
		//	{
		//		return BadRequest("Invalid request body.");
		//	}

		//	var response = _productService.UpdateProductStatus(id, model.Status);

		//	if (response.Success)
		//	{
		//		return Ok(response.Message);
		//	}

		//	return BadRequest(response.Message);
		//}

		public ResponseModel<BoolResponse> UpdateStatus(Guid id, UpdateProductStatusModel model)
		{
			var response = new ResponseModel<BoolResponse>();
			try
			{
				var result = _productService.UpdateProductStatus(id, model);
				response.Status = "Success";
				response.StatusCode= (int)HttpStatusCode.OK;
				response.Result = result;
				response.Message = result.Message;
			}
			catch (Exception ex) 
			{
				response.Status = "Error";
				response.StatusCode =(int)HttpStatusCode.InternalServerError;
				response.Message= ex.Message;
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
