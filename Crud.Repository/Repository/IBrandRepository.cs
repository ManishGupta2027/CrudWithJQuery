using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crud.Data.Entities;
using Crud.Data.Entities.Brand;

namespace Crud.Data.Repository
{
    public interface IBrandRepository
	{
		BoolResponse SaveBrand(Brand brand);
		Brand GetBrandListById(int id);
		BoolResponse UpsertBrand(Brand brand);
		List<Brand> GetBrandList(int currentPage, int pageSize);
		BoolResponse DeleteBrand(int id);
	}
}
