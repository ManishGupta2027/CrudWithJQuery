using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CrudOperation.Data;
using CrudOperation.Models;
using CrudOperation.Service;

namespace CrudOperation.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }



		//public ActionResult GetProductList()
		//{
		//	var res = _productService.GetProductList();

		//	return View(res);
		//}


		public ActionResult GetProductList(int? page)
		{
			int currentPage = (page ?? 1); // If no page number is specified, default to the first page
			int pageSize = 5; // Number of items per page

			// Retrieve the list of users from your repository
			var products = _productService.GetProductList(currentPage, pageSize);

			// Get the total number of users
			int totalProductsCount = products.FirstOrDefault().TotalRecords;

			// Calculate the total number of pages
			int totalPages = (int)Math.Ceiling((double)totalProductsCount / pageSize);

			// Pass necessary pagination information to the view
			ViewBag.TotalPages = totalPages;
			ViewBag.CurrentPage = currentPage;

			return View(products);
		}


		public ActionResult NewProduct()
        {
            return View();
        }

        [HttpPost]
        public JsonResult NewProduct(Product product)
        {
            var result = _productService.UpsertProduct(product);
            return Json(result,JsonRequestBehavior.AllowGet);

		}

		[HttpPost]
		public JsonResult UpsertProduct(Product product)
		{

			var result = _productService.UpsertProduct(product);
			return Json(result, JsonRequestBehavior.AllowGet);

		}

		[HttpGet]
		public ActionResult Edit(int id)
		{
			var user = _productService.GetProductListById(id);
			if (user == null)
			{
				return HttpNotFound(); // Or some other appropriate action
			}
			return View(user);
		}



		[HttpGet]
		public ActionResult Delete(int id)
		{
			var result = _productService.DeleteProduct(id);
			if (result)
			{
				// Optionally, you can redirect to a success page or refresh the user list
				return RedirectToAction("GetProductList");
			}
			// Optionally handle the case where delete fails
			return RedirectToAction("GetProductList");
		}
		public ActionResult NewProductSuccess()
        {
            return View();
        }
    }
}