using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrudOperation.Models.Tray
{
    public class TrayGroupUpdateModel: TrayGroupAddModel
    {
        public Guid RecordId { get; set; }
    }
}