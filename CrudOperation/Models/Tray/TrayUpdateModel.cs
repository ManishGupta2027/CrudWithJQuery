using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrudOperation.Models.Tray
{
    public class TrayUpdateModel : TrayAddModel
    {
        public Guid RecordId { get; set; }
    }
}