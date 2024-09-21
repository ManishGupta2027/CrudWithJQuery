using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud.Data.Entities.Product
{
    public class Product : BaseEntity
    {

        public string Title { get; set; }
        public string StockCode { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public string Gender { get; set; }
        public bool IsActive { get; set; }
   

    }
}
