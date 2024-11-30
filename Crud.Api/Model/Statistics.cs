namespace Crud.Api.Model
{
	public class Statistics
	{
			public List<ProductStatistics> ProductStatics { get; set; }
	}

		public class ProductStatistics
		{
			public string Key { get; set; }
			public string Value { get; set; }
		}
}
