namespace FreelanceBoard.MVC.Extensions
{
	public class OperationExecutor
	{
		private readonly ILogger<OperationExecutor> _logger;

		public OperationExecutor(ILogger<OperationExecutor> logger)
		{
			_logger = logger;
		}

		public async Task<bool> Execute(Func<Task> operation, Action<string> onError = null)
		{
			try
			{
				await operation();
				return true;
			}
			catch (ApplicationException ex)
			{
				onError?.Invoke(ex.Message);
				return false;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Unhandled error during operation.");
				onError?.Invoke("An unexpected error occurred.");
				return false;
			}
		}


	}
}
