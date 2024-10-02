using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud.Data.Entities.Asset
{
    public class RootFoldersResult
    {
        public List<Folder> Folders { get; set; }
        public string NextCursor { get; set; }
        public int TotalCount { get; set; }
    }

    public class Folder
    {
        public string Name { get; set; }
        public string Path { get; set; }
        //public string ExternalId { get; set; }
    }

}
