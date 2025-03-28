using CrudOperation.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrudOperation.Models.Tray
{
    public class TrayListModel: BaseEntity
    {
        public Guid RecordId { get; set; }
        public string Name { get; set; }
    }
}