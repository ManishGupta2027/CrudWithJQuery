using AutoMapper;
using Crud.Api.Model;
using Crud.Api.Model.Category;
using Crud.Api.Model.List;
using Crud.Service.Service.List;
using Microsoft.AspNetCore.Mvc;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Crud.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListController : ControllerBase
    {
        private IListService _listService;
        private readonly IMapper _mapper;
        public ListController(  IMapper mapper, IListService listService)
        {
            _mapper=mapper;
            _listService = listService;
        }
        // GET: api/<ListController>
        [HttpGet]
        [Route("category")]
        public ResponsecPaginationModel<List<ListModel>> GetAllCategory()
        {
            var response = new ResponsecPaginationModel<List<ListModel>>();
            var result = _listService.GetAllCategory(1,10000);
            var mappedCategoryList = _mapper.Map<List<ListModel>>(result);
            // Prepare a successful response
            response.Status = "Success";
            response.StatusCode = (int)HttpStatusCode.OK; // Using HttpStatusCode
            response.Result = mappedCategoryList;
            response.TotalRecords = result[0].TotalRecords ?? 0;
            response.CurrentPage = 0;
            response.PageSize = 0;
            return response;
        }

        [HttpGet]
        [Route("brand")]
        public ResponsecPaginationModel<List<ListModel>> GetAllBrand()
        {
            var response = new ResponsecPaginationModel<List<ListModel>>();
			var result = _listService.GetAllCategory(1, 10000);
			var mappedCategoryList = _mapper.Map<List<ListModel>>(result);

			response.Status = "Success";
			response.StatusCode = (int)HttpStatusCode.OK;
			response.Result = mappedCategoryList;
			response.TotalRecords = result[0].TotalRecords ?? 0;
			response.CurrentPage = 0;
			response.PageSize = 0;
			return response;
        }

        // POST api/<ListController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ListController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ListController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
