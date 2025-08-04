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
using Microsoft.Extensions.Logging;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace FreelanceBoard.Core.CommandHandlers.UserCommandHandlers
{
	public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Result<ApplicationUser>>
	{
		private readonly IUserRepository _userRepository;
		private readonly IMapper _mapper;
		private readonly OperationExecutor _executor;
		private readonly ILogger<UpdateUserCommandHandler> _logger;
		private readonly string UpdateOperation;

		public UpdateUserCommandHandler(IUserRepository userRepository, IMapper mapper, ILogger<UpdateUserCommandHandler> logger, OperationExecutor executor)
		{
			_userRepository = userRepository;
			_mapper = mapper;
			_logger = logger;
			_executor = executor;
			UpdateOperation = OperationType.Update.ToString();
		}
		public async Task<Result<ApplicationUser>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
		{
			_logger.LogInformation("Starting user update process...");

			if (request == null)
			{
				_logger.LogWarning("UpdateUserCommand is null.");
				return Result<ApplicationUser>.Failure(UpdateOperation, "Invalid request.");
			}

			if (string.IsNullOrWhiteSpace(request.Id))
			{
				_logger.LogWarning("User ID is missing in the update request.");
				return Result<ApplicationUser>.Failure(UpdateOperation, "User ID is required.");
			}

			return await _executor.Execute(async () =>
			{
				var user = await _userRepository.GetByIdAsync(request.Id);

				if (user == null)
				{
					_logger.LogWarning("User with ID {UserId} not found.", request.Id);
					return Result<ApplicationUser>.Failure(UpdateOperation, "User not found.");
				}

				_mapper.Map(request, user);

				await _userRepository.UpdateAsync(user);

				_logger.LogInformation("User with ID {UserId} updated successfully.", request.Id);
				return Result<ApplicationUser>.Success(user, UpdateOperation, "User updated successfully.");

			}, OperationType.Update);
		}

	}

}
