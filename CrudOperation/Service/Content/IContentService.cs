using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudOperation.Service.Content
{
    public interface IContentService
    {
        byte[] GetJsReportContent(object objData);
    }
}
