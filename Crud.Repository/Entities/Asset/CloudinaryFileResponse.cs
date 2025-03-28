using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud.Data.Entities.Asset
{
    public class CloudinaryFileResponse
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public bool IsDirectory { get; set; }
        public string DateModified { get; set; }
        public long Size { get; set; }
        public bool HasSubDirectories { get; set; }
        public string Url { get; set; }
    }
}
