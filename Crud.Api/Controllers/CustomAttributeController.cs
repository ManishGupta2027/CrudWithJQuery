using Crud.Api.Model.Brand;
using Crud.Api.Model;
using Crud.Data.Entities.Brand;
using Crud.Data.Entities;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Crud.Api.Model.ProductCustomField;
using AutoMapper;
using Crud.Service.BrandService;
using Crud.Service.ProductCustomFieldService;
using Crud.Service.ProductCustomfieldService;
using Crud.Data.Entities.ProductCustomField;
using Crud.Data.Entities.CustomAttributeSet;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Crud.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CustomAttributeController : ControllerBase
	{
		private IProductCustomFieldService _productCustomFieldService;
		private readonly IMapper _mapper;
		public CustomAttributeController(IProductCustomFieldService productCustomFieldService, IMapper mapper)
		{
			_mapper = mapper;
			_productCustomFieldService = productCustomFieldService;

		}
	
		// GET: api/<ProductCustomFieldController>
		[HttpGet]
		public ResponsecPaginationModel<List<ProductCustomFieldListModel>> GetAll(string name ,int currentPage=1, int pageSize = 40)
		{
			var response = new ResponsecPaginationModel<List<ProductCustomFieldListModel>>();
			var productCustomFieldlist = _productCustomFieldService.GetProductCustomFieldList(currentPage, pageSize);
			var mappedProductCustomFieldList = _mapper.Map<List<ProductCustomFieldListModel>>(productCustomFieldlist);
			// Prepare a successful response
			response.Status = "Success";
			response.StatusCode = (int)HttpStatusCode.OK; // Using HttpStatusCode
			response.Result = mappedProductCustomFieldList;
			response.TotalRecords = productCustomFieldlist[0].TotalRecords ?? 0;
			response.CurrentPage = currentPage;
			response.PageSize = pageSize;
			return response;
		}

		// GET api/<ProductCustomFieldController>/5
		[HttpGet("{id}")]
		public ResponseModel<ProductCustomFieldDetailModel> Get(Guid id)
		{
			var response = new ResponseModel<ProductCustomFieldDetailModel>();
			try
			{
				var result = _productCustomFieldService.GetProductCustomFieldById(id);
				var mappedProductCustomField = _mapper.Map<ProductCustomFieldDetailModel>(result);
				// Prepare a successful response
				response.Status = "Success";
				response.StatusCode = (int)HttpStatusCode.OK; // Using HttpStatusCode
				response.Result = mappedProductCustomField;
				//response.Message = result.Message;
			}
			catch (Exception ex)
			{
				// Prepare a failure response
				response.Status = "Error";
				response.StatusCode = (int)HttpStatusCode.InternalServerError; // Using HttpStatusCode
				response.Message = "An error occurred while saving the productCustomField.";
				//response.ErrorDetails.Add(ex.Message);

			}
			return response;
		}

		// POST api/<ProductCustomFieldController>
		[HttpPost]
		public ResponseModel<BoolResponse> Post(ProductCustomFieldModel model)
		{
			var response = new ResponseModel<BoolResponse>();
			try
			{
				// The below comment line tell the what is the error in response
				// Map ProductModel to Product entity
                var mappedProductCustomField = _mapper.Map<ProductCustomField>(model);

				// Save the product using the service
				var result = _productCustomFieldService.UpsertProductCustomField(mappedProductCustomField);

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

		// PUT api/<ProductCustomFieldController>/5
		[HttpPut]
		public ResponseModel<BoolResponse> Put(UpdateProductCustomFieldModel model)
		{
			var response = new ResponseModel<BoolResponse>();
			try
			{
				response.ErrorDetails = new List<string>();
				var mappedProductCustomField = _mapper.Map<ProductCustomField>(model);
				var result = _productCustomFieldService.UpsertProductCustomField(mappedProductCustomField);
				response.Status = "Success";
				response.StatusCode = (int)HttpStatusCode.OK;
				response.Result = result;
				response.Message = result.Message;
			}
			catch (Exception ex)
			{
				response.Status = "Error";
				response.StatusCode = (int)HttpStatusCode.InternalServerError;
				response.Message = "An error occurred while saving the productCustomField";
				response.ErrorDetails.Add(ex.Message);


			}
			return response;
		}

		// DELETE api/<ProductCustomFieldController>/5
		[HttpDelete("{id}")]
		public ResponseModel<BoolResponse> Delete(Guid id)
		{


			var response = new ResponseModel<BoolResponse>();
			try
			{
				// Save the product using the service
				var result = _productCustomFieldService.DeleteProductCustomField(id);

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

		//                        CustomAttriuteSet 


		// GET: api/<ProductCustomFieldController>
		[HttpGet("Set")]
		public ResponsecPaginationModel<List<CustomAttributeSetListModel>> GetAllCustomAttributeSets(string setName,int currentPage = 1 , int pageSize = 40)
		{
			var response = new ResponsecPaginationModel<List<CustomAttributeSetListModel>>();
			var customAttriuteSetList = _productCustomFieldService.GetCustomAttributeSetList(currentPage, pageSize, setName);
			var mappedCustomAttriuteSetList = _mapper.Map<List<CustomAttributeSetListModel>>(customAttriuteSetList);
			// Prepare a successful response
			response.Status = "Success";
			response.StatusCode = (int)HttpStatusCode.OK; // Using HttpStatusCode
			response.Result = mappedCustomAttriuteSetList;
			//response.TotalRecords = customAttriuteSetList[0].TotalRecords ?? 0;
			response.TotalRecords = customAttriuteSetList[0].TotalRecords ?? 0;
			response.CurrentPage = currentPage;
			response.PageSize = pageSize;
			return response;
		}

		// GET api/<ProductCustomFieldController>/5
		[HttpGet("Set/{id}")]
		public ResponseModel<CustomAttributeSetDetailModel> GetCustomAttributeSetById(Guid id)
		{
			var response = new ResponseModel<CustomAttributeSetDetailModel>();
			try
			{
				var result = _productCustomFieldService.GetCustomAttributeSetById(id);
				var mappedCustomAttriuteSet = _mapper.Map<CustomAttributeSetDetailModel>(result);
				// Prepare a successful response
				response.Status = "Success";
				response.StatusCode = (int)HttpStatusCode.OK; // Using HttpStatusCode
				response.Result = mappedCustomAttriuteSet;
				//response.Message = result.Message;
			}
			catch (Exception ex)
			{
				// Prepare a failure response
				response.Status = "Error";
				response.StatusCode = (int)HttpStatusCode.InternalServerError; // Using HttpStatusCode
				response.Message = "An error occurred while saving the customAttributeSet.";
				//response.ErrorDetails.Add(ex.Message);

			}
			return response;
		}

		// POST api/<ProductCustomFieldController>
		[HttpPost("Set")]
		public ResponseModel<BoolResponse> CreateCustomAttributeSet(CustomAttributeSetModel model)
		{
			var response = new ResponseModel<BoolResponse>();
			try
			{
				// The below comment line tell the what is the error in response
				// Map ProductModel to Product entity
				var mappedCustomAttributeSet = _mapper.Map<CustomAttributeSet>(model);

				// Save the product using the service
				var result = _productCustomFieldService.UpsertCustomAttributeSet(mappedCustomAttributeSet);

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

		// PUT api/<ProductCustomFieldController>/5
		[HttpPut("Set/{id}")]
		public ResponseModel<BoolResponse> PutCustomAttributeSet(UpdateCustomAttributeSet model)
		{
			var response = new ResponseModel<BoolResponse>();
			try
			{
				response.ErrorDetails = new List<string>();
				var mappedCustomAttriuteSet = _mapper.Map<CustomAttributeSet>(model);
				var result = _productCustomFieldService.UpsertCustomAttributeSet(mappedCustomAttriuteSet);
				response.Status = "Success";
				response.StatusCode = (int)HttpStatusCode.OK;
				response.Result = result;
				response.Message = result.Message;
			}
			catch (Exception ex)
			{
				response.Status = "Error";
				response.StatusCode = (int)HttpStatusCode.InternalServerError;
				response.Message = "An error occurred while saving the customAttributeSet";
				response.ErrorDetails.Add(ex.Message);


			}
			return response;
		}


		// DELETE api/<ProductCustomFieldController>/5
		[HttpDelete("Set/{id}")]
		public ResponseModel<BoolResponse> DeleteCustomAttributeSet(Guid id)
		{


			var response = new ResponseModel<BoolResponse>();
			try
			{
				// Save the product using the service
				var result = _productCustomFieldService.DeleteCustomAttributeSet(id);

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
