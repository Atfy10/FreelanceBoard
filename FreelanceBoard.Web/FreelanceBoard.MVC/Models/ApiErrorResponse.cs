namespace FreelanceBoard.MVC.Models
{
	public class ApiErrorResponse<T>
	{
		public bool IsSuccess { get; set; }
		public string Operation { get; set; }
		public string Message { get; set; }
		public T? Data { get; set; }
	}
}
