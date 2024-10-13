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
        public string LogoPreview { get; set; }
        public Configuration Flags { get; set; }    
        public List<Image> Images { get; set; }
        public List<Video> Videos { get; set; }


    }
}
