using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud.Data.Enums
{
	public enum ProductStatus
	{
		None = 0,
        Draft = 1,
        Active = 2,
        Archived= 3,
        Discontinued= 4,
        Pending = 5,
    }
}
