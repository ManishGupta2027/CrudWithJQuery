﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crud.Data.Entities;

namespace Crud.Data.Repository
{
	public interface IBrandRepository
	{
		BoolResponse SaveBrand(Brand brand);
		Brand GetBrandListById(int id);
	}
}
