using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrudOperation.Models
{
	public class Statistics
	{
		public List<ProductStatics> ProductStatics { get; set; }
	}

	public class ProductStatics
	{ 
	public string Key { get; set; }
	public string Value { get; set; }
	}

}