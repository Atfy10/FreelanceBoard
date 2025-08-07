namespace FreelanceBoard.MVC.Models
{
	public class ApiErrorResponse
	{
		public bool IsSuccess { get; set; }
		public string Operation { get; set; }
		public string Message { get; set; }
		public object? Data { get; set; }
	}
}
