using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FreelanceBoard.Core.Commands.UserCommands;
using FreelanceBoard.Core.Domain.Entities;
using FreelanceBoard.Core.Domain.Enums;
using FreelanceBoard.Core.Helpers;
using FreelanceBoard.Core.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace FreelanceBoard.Core.CommandHandlers.UserCommandHandlers
{
	public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Result<ApplicationUser>>
	{
		private readonly IUserRepository _userRepository;
		private readonly ILogger<CreateUserCommandHandler> _logger;
		private readonly OperationExecutor _executor;
		private readonly string DeleleOperation;


		public DeleteUserCommandHandler(IUserRepository userRepository, ILogger<CreateUserCommandHandler> logger,OperationExecutor executor)
		{
			_userRepository = userRepository;
			_logger = logger;
			_executor = executor;
			DeleleOperation = OperationType.Delete.ToString();
		}

		public async Task<Result<ApplicationUser>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
		{
			_logger.LogInformation("Starting user deletion process...");
			if (request == null)
			{
				_logger.LogWarning("DeleteUserCommand is null.");
				return Result<ApplicationUser>.Failure(DeleleOperation, "Invalid request.");
			}
			if (string.IsNullOrWhiteSpace(request.UserId))
			{
				_logger.LogWarning("User ID is missing in the delete request.");
				return Result<ApplicationUser>.Failure(DeleleOperation, "User ID is required.");
			}
			return await _executor.Execute(async () =>
			{
				var user = await _userRepository.GetByIdAsync(request.UserId);
				if (user == null)
				{
					_logger.LogWarning("User with ID {UserId} not found.", request.UserId);
					return Result<ApplicationUser>.Failure(DeleleOperation, "User not found.");
				}

				await _userRepository.DeleteAsync(request.UserId);
				_logger.LogInformation("User with ID {UserId} deleted successfully.", request.UserId);

				return Result<ApplicationUser>.Success(user, DeleleOperation, "User deleted successfully.");

			}, OperationType.Delete);
			
		}
	}
}
