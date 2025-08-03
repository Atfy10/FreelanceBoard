using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FreelanceBoard.Core.Commands;
using FreelanceBoard.Core.Domain.Entities;
using FreelanceBoard.Core.Interfaces;
using FreelanceBoard.Core.Services.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace FreelanceBoard.Core.Services.Implementations
{
	public class UserService : IUserService
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IMapper _mapper;
		private readonly ILogger<UserService> _logger;
		private readonly IUserRepository _userRepository;


		public UserService(UserManager<ApplicationUser> userManager, IMapper mapper, ILogger<UserService> logger , IUserRepository userRepository)
		{
			_userManager = userManager;
			_mapper = mapper;
			_logger = logger;
			_userRepository = userRepository;
		}

		public async Task<string> CreateUserAsync(CreateUserCommand command)
		{
			var user = _mapper.Map<ApplicationUser>(command);

			var result = await _userManager.CreateAsync(user, command.Password);

			if (!result.Succeeded)
			{
				var errors = string.Join(", ", result.Errors.Select(e => e.Description));
				_logger.LogError("Failed to create user: {Errors}", errors);
				throw new Exception($"Failed to create user: {errors}");
			}

			_logger.LogInformation("User created successfully with ID: {UserId}", user.Id);
			return user.Id;
		}

		public async Task<bool> DeleteUserAsync(DeleteUserCommand command)
		{
			if (string.IsNullOrWhiteSpace(command.UserId))
			{
				_logger.LogError("UserId cannot be null or empty.");
				return false;
			}
			try
			{
				var user = await _userRepository.GetByIdAsync(command.UserId);
				if (user == null)
				{
					_logger.LogWarning("User with ID {UserId} not found.", command.UserId);
					return false;
				}
				await _userRepository.DeleteAsync(command.UserId);
				_logger.LogInformation("User with ID {UserId} deleted successfully.", command.UserId);
				return true;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "An error occurred while deleting user with ID {UserId}.", command.UserId);
				return false;
			}
		}

		public async Task<bool> UpdateUserAsync(UpdateUserCommand command)
		{
			var user = _mapper.Map<ApplicationUser>(command);

			var userId = user.Id;

			var existingUser = await _userRepository.GetByIdAsync(userId);
			if (existingUser is null)
			{
				_logger.LogWarning("User with ID {UserId} not found", userId);
				return false;
			}

			await _userRepository.UpdateAsync(existingUser);

			_logger.LogInformation("User with ID {UserId} updated successfully", userId);
			return true;
		}
	}
}
