using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crud.Data.Entities;
using Crud.Data.Entities.Category;
using Crud.Data.Repository;
using Crud.Service.BrandService;

namespace Crud.Service.CategoryService
{
    public class CategoryService : ICategoryService
	{
		private readonly ICategoryRepository _categoryRepository;
		public CategoryService(ICategoryRepository categoryRepository)
		{
			_categoryRepository = categoryRepository;

		}

		public BoolResponse SaveCategory(Category category)
		{
			var res = _categoryRepository.SaveCategory(category);
			return res;
		}
		public Category GetCategoryListById(Guid id)
		{
			return _categoryRepository.GetCategoryListById(id);
		}
		public BoolResponse UpsertCategory(Category category)
		{
			return _categoryRepository.UpsertCategory(category);
		}
		public List<Category> GetCategoryList(int currentPage, int pageSize)
		{
			return _categoryRepository.GetCategoryList(currentPage, pageSize);
		}
		public BoolResponse DeleteCategory(Guid id)
		{
			return _categoryRepository.DeleteCategory(id);
		}
	}
}
