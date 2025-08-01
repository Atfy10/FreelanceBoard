using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FreelanceBoard.Core.Commands;
using FreelanceBoard.Core.Domain.Entities;
using FreelanceBoard.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FreelanceBoard.Core.CommandHandlers
{
	public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
	{
		private readonly IUserRepository _userRepository;
		private readonly ILogger<DeleteUserCommandHandler> _logger;
		public DeleteUserCommandHandler(IUserRepository userRepository, ILogger<DeleteUserCommandHandler> logger)
		{
			_userRepository = userRepository;
			_logger = logger;
		}

		public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
		{
			if (string.IsNullOrWhiteSpace(request.UserId))
			{
				_logger.LogError("UserId cannot be null or empty.");
				return false;
			}
			try
			{
				var user = await _userRepository.GetByIdAsync(request.UserId);
				if (user == null)
				{
					_logger.LogWarning("User with ID {UserId} not found.", request.UserId);
					return false;
				}
				await _userRepository.DeleteAsync(request.UserId);
				_logger.LogInformation("User with ID {UserId} deleted successfully.", request.UserId);
				return true;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "An error occurred while deleting user with ID {UserId}.", request.UserId);
				return false;
			}
		}

	}
}
