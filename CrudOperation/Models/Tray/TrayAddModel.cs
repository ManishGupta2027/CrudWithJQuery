using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrudOperation.Models.Tray
{
    public class TrayAddModel
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string BarCode { get; set; }
        public Guid DeliveryCenterId { get; set; }
    }
}