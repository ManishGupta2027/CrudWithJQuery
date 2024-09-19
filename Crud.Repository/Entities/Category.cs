using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud.Data.Entities
{
	public class Category
	{
		public int? Id { get; set; }
		public string Name { get; set; }
		public string Code { get; set; }
		public DateTime? Created { get; set; }
		public string CreatedBy { get; set; }
		public DateTime LastUpdated { get; set; }
		public string LastUpdatedBy { get; set;}
	}
}
