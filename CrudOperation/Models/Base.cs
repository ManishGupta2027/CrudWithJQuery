﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrudOperation.Models
{
	public class Base
	{
		public int CurrentPage { get; set; }
		public int PageSize { get; set; }
		public int TotalRecords { get; set; }
	}
}