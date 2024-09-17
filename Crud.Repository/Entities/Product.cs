﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud.Data.Entities
{
	public class Product
	{
		public int? Id { get; set; }

		public string Title { get; set; }
		public string StockCode { get; set; }
		public decimal Price { get; set; }

		public string Category { get; set; }
		public string Gender { get; set; }
		public bool IsActive { get; set; }
		public int? CurrentPage { get; set; }
		public int? PageSize { get; set; }
		public int? TotalRecords { get; set; }
		public DateTime? Created { get; set; }

	}
}
