using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud.Data.Entities.Asset
{
    public class UploadRequest
    {
        public string Base64 { get; set; }
        public string FileName { get; set; }
        public string FolderName { get; set; }
    }
}
