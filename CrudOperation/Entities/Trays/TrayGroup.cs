using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrudOperation.Entities.Trays
{
    public class TrayGroup : BaseEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public int Capacity { get; set; }
        public string BarCode { get; set; }
        public List<Guid> TrayIds { get; set; }
        public List<SelectedTray> Trays { get; set; }
        public Guid DeliveryCenterId { get; set; }
    }
    public class SelectedTray 
    {
        public Guid RecordId { get; set; }
        public string Name { get; set; }
    }
}