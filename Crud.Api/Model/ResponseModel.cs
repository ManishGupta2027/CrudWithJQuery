namespace Crud.Api.Model
{
	public class ResponseModel<T>
	{
		public string Status { get; set; }
		public int StatusCode { get; set; }
		public T Result { get; set; }
		public string Message { get; set; }
		public List<string> ErrorDetails { get; set; }
	}
	public class ResponsecPaginationModel<T> : ResponseModel<T>
	{
		public int CurrentPage { get; set;}
		public int PageSize { get; set; }
		public int TotalRecords { get; set; }
	}
}
