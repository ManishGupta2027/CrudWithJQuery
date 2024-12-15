using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrudOperation.Entities.Trays
{
    public class TrayList: BaseEntity
    {
        public Guid RecordId { get; set; }
        public string Name { get; set; }
    }
}