using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrudOperation.Models
{
    public class DeliveryCenterDetailModel
    {
       // public Guid RecordId { get; set; }
        public string Code { get; set; }
        //  public string Name { get; set; }
        public Guid DeliveryCenterId { get; set; }

        public string DeliveryCenterName { get; set; }
    }
}