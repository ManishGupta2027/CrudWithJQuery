using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud.Data.Entities
{
	public class Brand
	{
		public int? Id { get; set; }
		public string Name { get; set; }
		public string ShortDescription { get; set; }
		public string Description { get; set; }

		public DateTime? Created { get; set; }
		public DateTime? Updated { get; set; }
		public DateTime? LastCreated { get; set; }
		public DateTime? LastUpdated { get; set; }
	}
}
