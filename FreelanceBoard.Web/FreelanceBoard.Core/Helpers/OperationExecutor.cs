using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreelanceBoard.Core.Domain.Enums;
using FreelanceBoard.Core.Exceptions;
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
            Result<T> res;
            try
            {
                _logger.LogInformation("Starting {Operation} operation...", opType);
                res = await operation();
                _logger.LogInformation("{Operation} operation completed successfully.", opType);

                if (res.IsSuccess)
                    _logger.LogInformation("Operation {Operation} success: {Message}", opType, res.Message);
                else
                    _logger.LogWarning("Operation {Operation} failed: {Message}", opType, res.Message);

                return res;
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogError(ex, "Argument null error occurred during {Operation}", opType);
                return Result<T>.Failure(opType.ToString(), "An arg was null: " + ex.Message);
            }
            catch (NullReferenceException ex)
            {
                _logger.LogError(ex, "Null reference error occurred during {Operation}", opType);
                return Result<T>.Failure(opType.ToString(), "A null reference was encountered: " + ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, "Invalid operation error occurred during {Operation}", opType);
                return Result<T>.Failure(opType.ToString(), "An invalid operation was attempted: " + ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogError(ex, "Key not found error occurred during {Operation}", opType);
                return Result<T>.Failure(opType.ToString(), "The key is not found: " + ex.Message);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Database error occurred during {Operation}", opType);
                return Result<T>.Failure(opType.ToString(), "A database error occurred: " + ex.Message);
            }
            catch (EmailNotFoundException ex)
            {
                _logger.LogError(ex, "Email not found error occurred during {Operation}", opType);
                return Result<T>.Failure(opType.ToString(), "Email not found: " + ex.Message);
            }
            catch (EmailExistException ex)
            {
                _logger.LogError(ex, "Email already exist error occurred during {Operation}", opType);
                return Result<T>.Failure(opType.ToString(), "Email is exist: " + ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred during {Operation}", opType);
                return Result<T>.Failure(opType.ToString(), "An unexpected error occurred: " + ex.Message);
            }
        }
    }
}
