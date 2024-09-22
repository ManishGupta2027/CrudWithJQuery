using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud.Data.Entities
{
	public class BoolResponse
	{
		public bool IsValid { get; set; }
		public string Message { get; set; }
		public Guid? RecordId { get; set; }
	}
}
