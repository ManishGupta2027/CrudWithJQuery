using System.Net;
using AutoMapper;
using Crud.Api.Model;
using Crud.Data.Entities;
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
		public IEnumerable<string> Get()
		{
			return new string[] { "value1", "value2" };
		}

		// GET api/<BrandController>/5
		[HttpGet("{id}")]
		public ResponseModel<BrandDetailModel> Get(int id)
		{
			var response = new ResponseModel<BrandDetailModel>();
			try
			{
				var result = _brandService.GetBrandListById(id);
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
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] string value)
		{
		}

		// DELETE api/<BrandController>/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
	}
}
