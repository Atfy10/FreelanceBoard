using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreelanceBoard.Core.Commands.UserCommands;
using FreelanceBoard.Core.Domain.Entities;
using FreelanceBoard.Core.Domain.Enums;
using FreelanceBoard.Core.Helpers;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace FreelanceBoard.Core.CommandHandlers.UserCommandHandlers
{
	public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Result<string>>
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly ILogger<ChangePasswordCommandHandler> _logger;
		private readonly OperationExecutor _executor;
		private readonly string UpdateOperation = OperationType.Update.ToString();

		public ChangePasswordCommandHandler(UserManager<ApplicationUser> userManager,
			ILogger<ChangePasswordCommandHandler> logger,
			OperationExecutor executor)
		{
			_userManager = userManager;
			_logger = logger;
			_executor = executor;
		}

		public async Task<Result<string>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
		=> await _executor.Execute(async () =>
		{
			_logger.LogInformation("Starting password change process for user {UserId}...", request.UserId);
			if (request == null)
				throw new NullReferenceException("Change password request cannot be null.");
			if (string.IsNullOrWhiteSpace(request.UserId))
				throw new ArgumentNullException("User ID cannot be null or empty.");
			var user = await _userManager.FindByIdAsync(request.UserId) ??
					   throw new KeyNotFoundException($"User with ID {request.UserId} not found.");
			var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
			if (!result.Succeeded)
			{
				var errors = string.Join(", ", result.Errors.Select(e => e.Description));
				throw new InvalidOperationException($"Password change failed: {errors}");
			}
			_logger.LogInformation("Password for user {UserId} changed successfully.", request.UserId);
			return Result<string>.Success(null, UpdateOperation, "Password changed successfully.");
		}, OperationType.Update);
	}
}
