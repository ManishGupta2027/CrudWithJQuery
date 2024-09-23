using Crud.Data.Entities.Category;
using Crud.Data.Entities.List;
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

        public ListService(ICategoryService categoryService)
        {
            _categoryService= categoryService;
        }
        public List<Category> GetAllCategory(int currentPage, int pageSize)
        {
            var list = _categoryService.GetCategoryList(currentPage, pageSize);
            return list;

        }
    }
}
