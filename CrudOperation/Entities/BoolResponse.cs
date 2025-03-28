using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrudOperation.Entities
{
    public class BoolResponse
    {
        public bool IsValid { get; set; }
        public string Message { get; set; }
        public string MessageCode { get; set; }
        public Guid RecordId { get; set; }
      
    }
}