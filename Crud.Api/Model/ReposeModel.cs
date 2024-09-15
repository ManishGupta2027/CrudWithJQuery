namespace Crud.Api.Model
{
	public class ReposeModel<T>
	{
		public string Status { get; set; }
		public int StatusCode { get; set; }
		public T Result { get; set; }
		public string Message { get; set; }
		public List<string> ErrorDetails { get; set; }
	}
}
