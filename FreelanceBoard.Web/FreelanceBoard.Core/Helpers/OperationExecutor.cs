using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreelanceBoard.Core.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FreelanceBoard.Core.Helpers
{
	public class OperationExecutor
	{
		private readonly ILogger<OperationExecutor> _logger;

		public OperationExecutor(ILogger<OperationExecutor> logger)
		{
			_logger = logger;
		}

		public async Task<Result<T>> Execute<T>(Func<Task<Result<T>>> operation, OperationType opType)
		{
			try
			{
				return await operation();
			}
			catch (DbUpdateException ex)
			{
				_logger.LogError(ex, "Database error occurred during {Operation}", opType);
				return Result<T>.Failure(opType.ToString(), "A database error occurred: " + ex.Message);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Unexpected error occurred during {Operation}", opType);
				return Result<T>.Failure(opType.ToString(), "An unexpected error occurred: " + ex.Message);
			}
		}
	}
}
