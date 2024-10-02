﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud.Data.Entities.ProductCustomField
{
	public class ProductCustomField : BaseEntity
	{
		public string FieldCode { get; set; }
		public string FieldName { get; set; }
		public int InputType  { get; set; }
	}
}
