using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud.Data.Entities.CustomAttributeSet
{
	public class CustomAttributeSet : BaseEntity
	{
		public Guid? Id { get; set; }
		public string SetName { get; set; }
	}
}
