using System.Net;
using AutoMapper;
using Crud.Api.Model;
using Crud.Api.Model.Brand;
using Crud.Api.Model.Product;
using Crud.Data.Entities;
using Crud.Data.Entities.Brand;
using Crud.Service.BrandService;
using Microsoft.AspNetCore.Mvc;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Crud.Api.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class BrandController : ControllerBase
	{
		private IBrandService _brandService;
		private readonly IMapper _mapper;
        public BrandController(IBrandService brandService, IMapper mapper)
        {
			_mapper = mapper;
			_brandService = brandService;

		}

        // GET: api/<BrandController>
        [HttpGet]
		public ResponsecPaginationModel<List<BrandListModel>> GetAll(int currentPage, int pageSize = 40)
		{
			var response = new ResponsecPaginationModel<List<BrandListModel>>();
			var brandlist = _brandService.GetBrandList(currentPage, pageSize);
			var mappedBrandList = _mapper.Map<List<BrandListModel>>(brandlist);
			// Prepare a successful response
			response.Status = "Success";
			response.StatusCode = (int)HttpStatusCode.OK; // Using HttpStatusCode
			response.Result = mappedBrandList;
			response.TotalRecords = brandlist[0].TotalRecords ?? 0;
			response.CurrentPage = currentPage;
			response.PageSize = pageSize;
			return response;
		}

		// GET api/<BrandController>/5
		[HttpGet("{id}")]
		public ResponseModel<BrandDetailModel> Get(Guid id)
		{
			var response = new ResponseModel<BrandDetailModel>();
			try
			{
				var result = _brandService.GetBrandById(id);
				var mappedBrand = _mapper.Map<BrandDetailModel>(result);
				// Prepare a successful response
				response.Status = "Success";
				response.StatusCode = (int)HttpStatusCode.OK; // Using HttpStatusCode
				response.Result = mappedBrand;
				//response.Message = result.Message;
			}
			catch (Exception ex)
			{
				// Prepare a failure response
				response.Status = "Error";
				response.StatusCode = (int)HttpStatusCode.InternalServerError; // Using HttpStatusCode
				response.Message = "An error occurred while saving the brand.";
				//response.ErrorDetails.Add(ex.Message);

			}
			return response;
		}

		// POST api/<BrandController>
		[HttpPost]
		public ResponseModel<BoolResponse> Post(BrandModel model)
		{
			var response = new ResponseModel<BoolResponse>();
			try
			{
				// The below comment line tell the what is the error in response
				// Map ProductModel to Product entity
				var mappedBrand = _mapper.Map<Brand>(model);

				// Save the product using the service
				var result = _brandService.SaveBrand(mappedBrand);

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

		// PUT api/<BrandController>/5
		[HttpPut]
		public ResponseModel<BoolResponse> Put(UpdateBrandModel model)
		{
			var response = new ResponseModel<BoolResponse>();
			try
			{
				response.ErrorDetails = new List<string>();
				var mappedBrand = _mapper.Map<Brand>(model);
				var result = _brandService.UpsertBrand(mappedBrand);
				response.Status = "Success";
				response.StatusCode = (int)HttpStatusCode.OK;
				response.Result = result;	
				response.Message = result.Message;
			}
			catch(Exception ex)
			{
				response.Status = "Error";	
				response.StatusCode= (int)HttpStatusCode.InternalServerError;
				response.Message = "An error occurred while saving the brand";
				response.ErrorDetails.Add(ex.Message);


			}
			return response;
		}

		// DELETE api/<BrandController>/5
		[HttpDelete("{id}")]
		public ResponseModel<BoolResponse> Delete(Guid id)
		{


			var response = new ResponseModel<BoolResponse>();
			try
			{
				// Save the product using the service
				var result = _brandService.DeleteBrand(id);

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
