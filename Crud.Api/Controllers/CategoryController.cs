using System.Net;
using AutoMapper;
using Crud.Api.Model;
using Crud.Api.Model.Category;
using Crud.Data.Entities;
using Crud.Service.BrandService;
using Crud.Service.CategoryService;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Crud.Api.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class CategoryController : ControllerBase
	{
		private ICategoryService _categoryService;
		private readonly IMapper _mapper;
		public CategoryController(ICategoryService categoryService, IMapper mapper)
		{
			_mapper = mapper;
			_categoryService = categoryService;

		}
		[HttpGet]
		public ResponseModel<List<Category>> GetAll(int currentPage, int pageSize = 40)
		{
			var response = new ResponseModel<List<Category>>();
			var categorylist = _categoryService.GetCategoryList(currentPage, pageSize);
			// Prepare a successful response
			response.Status = "Success";
			response.StatusCode = (int)HttpStatusCode.OK; // Using HttpStatusCode
			response.Result = categorylist;
			return response;
		}

		// GET api/<CategoryController>/5
		[HttpGet("{id}")]
		public ResponseModel<CategoryDetailModel> Get(int id)
		{
			var response = new ResponseModel<CategoryDetailModel>();
			try
			{
				var result = _categoryService.GetCategoryListById(id);
				var mappedCategory = _mapper.Map<CategoryDetailModel>(result);
				// Prepare a successful response
				response.Status = "Success";
				response.StatusCode = (int)HttpStatusCode.OK; // Using HttpStatusCode
				response.Result = mappedCategory;
				//response.Message = result.Message;
			}
			catch (Exception ex)
			{
				// Prepare a failure response
				response.Status = "Error";
				response.StatusCode = (int)HttpStatusCode.InternalServerError; // Using HttpStatusCode
				response.Message = "An error occurred while saving the category.";
				//response.ErrorDetails.Add(ex.Message);

			}
			return response;
		}

		// POST api/<CategoryController>
		[HttpPost]
		public ResponseModel<BoolResponse> Post(CategoryModel model)
		{
			var response = new ResponseModel<BoolResponse>();
			try
			{
				// The below comment line tell the what is the error in response
			     response.ErrorDetails = new List<string>();
				// Map ProductModel to Product entity
				var mappedCategory = _mapper.Map<Category>(model);

				// Save the product using the service
				var result = _categoryService.SaveCategory(mappedCategory);

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
				response.Message = "An error occurred while saving the category.";
				response.ErrorDetails.Add(ex.Message);

			}
			return response;
		}

		// PUT api/<CategoryController>/5
		[HttpPut]
		public ResponseModel<BoolResponse> Put(UpdatedCategoryModel model)
		{
			          var response = new ResponseModel<BoolResponse>();
			try
			{
				response.ErrorDetails = new List<string>();
				var mappedCategory = _mapper.Map<Category>(model);
				var result = _categoryService.UpsertCategory(mappedCategory);
				response.Status = "Success";
				response.StatusCode = (int)HttpStatusCode.OK;
				response.Result = result;
				response.Message = result.Message;
			}
			catch (Exception ex)
			{
				response.Status = "Error";
				response.StatusCode = (int)HttpStatusCode.InternalServerError;
				response.Message = "An error occurred while saving the category";
				response.ErrorDetails.Add(ex.Message);


			}
			return response;
		}

		// DELETE api/<CategoryController>/5
		[HttpDelete("{id}")]
		public ResponseModel<BoolResponse> Delete(int id)
		{


			var response = new ResponseModel<BoolResponse>();
			try
			{
				// Save the product using the service
				var result = _categoryService.DeleteCategory(id);

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
				response.Message = "An error occurred while saving the category.";
				response.ErrorDetails.Add(ex.Message);

			}
			return response;
		}
	}
}
