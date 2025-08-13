using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

                if (res.IsSuccess)
                    _logger.LogInformation("{Operation} operation completed successfully: {Message}", opType, res.Message);
                else
                    _logger.LogWarning("Operation {Operation} failed: {Message}", opType, res.Message);

                return res;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                _logger.LogError(ex, "Argument out of range error occurred during {Operation}", opType);
                return Result<T>.Failure(opType.ToString(), "The provided value is out of the acceptable range. Please check and try again.");
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogError(ex, "Argument null error occurred during {Operation}", opType);
                return Result<T>.Failure(opType.ToString(), "Some required information was missing. Please check and try again.");
            }
            catch (NullReferenceException ex)
            {
                _logger.LogError(ex, "Null reference error occurred during {Operation}", opType);
                return Result<T>.Failure(opType.ToString(), "Something went wrong while processing your request. Please try again.");
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, "Invalid operation error occurred during {Operation}", opType);
                return Result<T>.Failure(opType.ToString(), "The action you tried isn’t allowed in the current state.");
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogError(ex, "Key not found error occurred during {Operation}", opType);
                return Result<T>.Failure(opType.ToString(), "We couldn’t find the requested item.");
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Database error occurred during {Operation}", opType);
                return Result<T>.Failure(opType.ToString(), "There was a problem saving your changes. Please try again later.");
            }
            catch (ValidationException ex)
            {
                _logger.LogError(ex, "Validation error occurred during {Operation}", opType);
                return Result<T>.Failure(opType.ToString(), "Some information you entered isn’t valid. Please review and try again.");
            }
            catch (InvalidSortChoiceException ex)
            {
                _logger.LogError(ex, "Invalid sort choice error occurred during {Operation}", opType);
                return Result<T>.Failure(opType.ToString(), "The sort option you selected isn’t available.");
            }
            catch (EmailNotFoundException ex)
            {
                _logger.LogError(ex, "Email not found error occurred during {Operation}", opType);
                return Result<T>.Failure(opType.ToString(), "We couldn’t find an account with that email.");
            }
            catch (EmailExistException ex)
            {
                _logger.LogError(ex, "Email already exists error occurred during {Operation}", opType);
                return Result<T>.Failure(opType.ToString(), "That email address is already in use.");
            }
            catch (PhoneExistException ex)
            {
                _logger.LogError(ex, "Phone already exist error occurred during {Operation}", opType);
                return Result<T>.Failure(opType.ToString(), ex.Message);
            }
            catch (UserNameExistException ex)
            {
                _logger.LogError(ex, "Username already exist error occurred during {Operation}", opType);
                return Result<T>.Failure(opType.ToString(), ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogError(ex, "Unauthorized access error occurred during {Operation}", opType);
                return Result<T>.Failure(opType.ToString(), "Unauthorized access: " + ex.Message);
            }


			catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred during {Operation}", opType);
                return Result<T>.Failure(opType.ToString(), "An unexpected error occurred. Please try again later.");
            }

        }
    }
}
