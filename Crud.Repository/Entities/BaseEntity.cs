using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud.Data.Entities
{
	public class BaseEntity
	{
		public Guid? Id { get; set; }
		public DateTime? Created { get; set; }
		public string CreatedBy { get; set; }
		public DateTime? LastUpdated { get; set; }
		public string LastUpdatedBy { get; set; }
		public int? CurrentPage { get; set; }
		public int? PageSize { get; set; }
		public int? TotalRecords { get; set; }
	}
}
