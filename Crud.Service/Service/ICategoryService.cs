using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crud.Data.Entities;

namespace Crud.Service.CategoryService
{
	public interface ICategoryService
	{
		BoolResponse SaveCategory(Category category);
		Category GetCategoryListById(int id);
		BoolResponse UpsertCategory(Category category);
		List<Category> GetCategoryList(int currentPage, int pageSize);
		BoolResponse DeleteCategory(int id);
	}
}
