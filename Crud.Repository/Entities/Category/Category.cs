using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud.Data.Entities.Category
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        
    }
}
