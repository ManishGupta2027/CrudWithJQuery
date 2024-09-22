using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud.Data.Entities.Brand
{
    public class Brand : BaseEntity
	{
      
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
      
    }
}
