using CrudOperation.Entities.Trays;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrudOperation.Models.Tray
{
    public class TrayGroupAddModel
    {
        public string Name { get; set; }
        public int Capacity { get; set; }
        public string BarCode { get; set; }
        public List<Guid> TrayIds { get; set; }
        public List<SelectedTray> Trays { get; set; }
        public Guid DeliveryCenterId { get; set; }
    }
}