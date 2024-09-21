using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crud.Data.Entities;
using Crud.Data.Entities.Category;

namespace Crud.Data.Repository
{
    public interface ICategoryRepository
	{
		BoolResponse SaveCategory(Category category);
		Category GetCategoryListById(int id);
		BoolResponse UpsertCategory(Category category);
		List<Category> GetCategoryList(int currentPage, int pageSize);
		BoolResponse DeleteCategory(int id);
	}
}
