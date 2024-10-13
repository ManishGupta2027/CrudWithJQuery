using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud.Data.Entities.Brand
{
    public class Video
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public int DisplayOrder { get; set; }
        public string Description { get; set; }
    }
}
