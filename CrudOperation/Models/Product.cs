using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrudOperation.Models
{
    public class Product
    {
        public string Title { get; set; }
        public string StockCode { get; set; }
        public int Price { get; set; }

        public string Category { get; set; }
       
    }
}