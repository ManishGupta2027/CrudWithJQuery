using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrudOperation.Models.Tray
{
    public class TrayDetailModel
    {
        public Guid RecordId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string BarCode { get; set; }
        public string Base64 { get; set; }
        public Guid DeliveryCenterId { get; set; }
    }
}