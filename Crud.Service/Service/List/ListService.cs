using Crud.Data.Entities.Brand;
using Crud.Data.Entities.Category;
using Crud.Data.Entities.List;
using Crud.Service.BrandService;
using Crud.Service.CategoryService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud.Service.Service.List
{
    public class ListService : IListService
    {
        private ICategoryService _categoryService;
		private readonly IBrandService _brandService;

		public ListService(ICategoryService categoryService, IBrandService brandService)
        {
            _categoryService= categoryService;
			_brandService = brandService;
		}
        public List<Category> GetAllCategory(int currentPage, int pageSize)
        {
            var list = _categoryService.GetCategoryList(currentPage, pageSize,null);
            return list;

        }
		public List<Brand> GetAllBrand(int currentPage, int pageSize)
		{
			var list = _brandService.GetBrandList(currentPage, pageSize);
			return list;
		}
	}
}
