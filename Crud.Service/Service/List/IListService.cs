using Crud.Data.Entities.Category;
using Crud.Data.Entities.List;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud.Service.Service.List
{
    public interface IListService
    {
        List<Category> GetAllCategory(int currentPage , int pageSize);
    }
}
