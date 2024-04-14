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



		public ActionResult GetProductList()
		{
			var res = _productService.GetProductList();

			return View(res);
		}


		public ActionResult NewProduct()
        {
            return View();
        }

        [HttpPost]
        public JsonResult NewProduct(Product product)
        {
            var result = _productService.SaveProduct(product);
            return Json(result,JsonRequestBehavior.AllowGet);

		}
		public ActionResult UpdateProduct()
		{
			return View();
		}
		public ActionResult NewProductSuccess()
        {
            return View();
        }
    }
}