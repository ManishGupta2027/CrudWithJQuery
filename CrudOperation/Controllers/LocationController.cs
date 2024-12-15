using CrudOperation.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CrudOperation.Controllers
{
    public class LocationController : Controller
    {
        // GET: Location
        public ActionResult Index()
        {
            return View(DemoData.DeliveryCenters);
        }
        public ActionResult Detail(Guid id)
        {
            var deliveryCenter = DemoData.DeliveryCenters.FirstOrDefault(dc => dc.DeliveryCenterId == id);

            if (deliveryCenter == null)
            {
                return HttpNotFound();
            }

            return View(deliveryCenter);
        }
    }
}