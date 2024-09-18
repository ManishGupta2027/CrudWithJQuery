using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crud.Data.Entities;
using Crud.Data.Repository;

namespace Crud.Service.BrandService
{
	public class BrandService : IBrandService
	{
		private readonly IBrandRepository _brandRepository;
		public BrandService(IBrandRepository brandRepository)
		{
			_brandRepository = brandRepository;

		}

		public BoolResponse SaveBrand(Brand brand)
		{
			var res = _brandRepository.SaveBrand(brand);
			return res;
		}
		public Brand GetBrandListById(int id)
		{
			return _brandRepository.GetBrandListById(id);
		}
		public BoolResponse UpsertBrand(Brand brand) 
		{
			return _brandRepository.UpsertBrand(brand);
		}
		public List<Brand> GetBrandList(int currentPage, int pageSize) 
		{
			return _brandRepository.GetBrandList(currentPage, pageSize);
		}
		public BoolResponse DeleteBrand(int id)
		{ 
			return _brandRepository.DeleteBrand(id);
		}
	}
}