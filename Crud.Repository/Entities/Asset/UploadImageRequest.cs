using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud.Data.Entities.Asset
{
    public class UploadImageRequest
    {
        public string Base64Image { get; set; }
        public string Folder { get; set; }
    }

}
