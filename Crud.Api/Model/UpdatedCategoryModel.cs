﻿namespace Crud.Api.Model
{
	public class UpdatedCategoryModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Code { get; set; }
		//public string CreatedBy { get; set; }
		//public DateTime LastUpdated { get; set; }
		public string LastUpdatedBy { get; set; }
	}
}