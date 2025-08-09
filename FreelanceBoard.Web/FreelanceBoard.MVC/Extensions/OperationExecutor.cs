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
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "Unhandled error during operation.");
                onError?.Invoke(ex.Message);
                return false;
            }
            catch (ApplicationException ex)
            {
                _logger.LogError(ex, "Unhandled error during operation.");
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

        //public async Task<(bool Success, T Data)> Execute<T>(
        //    Func<Task<T>> operation,
        //    Action<string> onError = null)
        //{
        //    try
        //    {
        //        var result = await operation();
        //        return (true, result);
        //    }
        //    catch (ApplicationException ex)
        //    {
        //        _logger.LogError(ex, "Handled error during operation.");
        //        onError?.Invoke(ex.Message);
        //        return (false, default);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Unhandled error during operation.");
        //        onError?.Invoke("An unexpected error occurred.");
        //        return (false, default);
        //    }
        //}

    }
}
