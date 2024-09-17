﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crud.Data.Entities;

namespace Crud.Service.BrandService
{
	public interface IBrandService
	{
		BoolResponse SaveBrand(Brand brand);
		Brand GetBrandListById(int id);
	}
}