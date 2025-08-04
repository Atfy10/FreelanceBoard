using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FreelanceBoard.Core.Domain.Entities;
using FreelanceBoard.Core.Domain.Enums;
using FreelanceBoard.Core.Dtos;
using FreelanceBoard.Core.Helpers;
using FreelanceBoard.Core.Interfaces;
using FreelanceBoard.Core.Queries.Interfaces;
using Microsoft.Extensions.Logging;

namespace FreelanceBoard.Core.Queries.Implementations
{
	public class UserQuery : IUserQuery
	{
		private readonly IUserRepository _userRepository;
		private readonly IMapper _mapper;
		private readonly ILogger<UserQuery> _logger;
		private readonly OperationExecutor _executor;

		private readonly string GetOperation;
		private readonly string GetALLOperation;


		public UserQuery(IUserRepository userRepository, IMapper mapper, ILogger<UserQuery> logger, OperationExecutor executor)
		{
			_userRepository = userRepository;
			_mapper = mapper;
			_logger = logger;
			_executor = executor;
			GetOperation= OperationType.Get.ToString();
			GetALLOperation= OperationType.GetAll.ToString();
		}

		public async Task<Result<IEnumerable<ApplicationUserDto>>> GetAllBannedUsersAsync()
		{
			_logger.LogInformation("Retrieving all banned users");
			return await _executor.Execute(async () =>
			{
				var allUsers = await _userRepository.GetAllAsync();
				var bannedUsers = allUsers.Where(u => u.IsBanned).ToList();
				if (!bannedUsers.Any())
				{
					_logger.LogInformation("No banned users found.");
					//write message in success result
					return Result<IEnumerable<ApplicationUserDto>>.Success(Enumerable.Empty<ApplicationUserDto>(), GetALLOperation, "No banned users found.");
				}
				_logger.LogInformation("Retrieved {Count} banned users.", bannedUsers.Count);
				var bannedUserDtos = _mapper.Map<IEnumerable<ApplicationUserDto>>(bannedUsers);
				return Result<IEnumerable<ApplicationUserDto>>.Success(bannedUserDtos, GetALLOperation, "Banned Users retreived successfully");
			}, OperationType.GetAll);
		}

		public async Task<Result<IEnumerable<ApplicationUserDto>>> GetAllUsersAsync()
		{
			_logger.LogInformation("Retrieving all users");
			return await _executor.Execute(async () =>
			{
				var allUsers = await _userRepository.GetAllAsync();
				if (!allUsers.Any())
				{
					_logger.LogInformation("No users found.");
					return Result<IEnumerable<ApplicationUserDto>>.Success(Enumerable.Empty<ApplicationUserDto>(), GetALLOperation, "No users found.");
				}
				var userDtos = _mapper.Map<IEnumerable<ApplicationUserDto>>(allUsers);
				return Result<IEnumerable<ApplicationUserDto>>.Success(userDtos, GetALLOperation, "Users retrieved successfully ");
			}, OperationType.GetAll);
		}

		public async Task<Result<ApplicationUserDto>> GetUserByIdAsync(string id)
		{
			_logger.LogInformation("Retrieving user by Id");
			return await _executor.Execute(async () =>
			{
				if (string.IsNullOrWhiteSpace(id))
				{
					return Result<ApplicationUserDto>.Failure(GetOperation, "User ID must be provided.");
				}

				var user = await _userRepository.GetByIdAsync(id);

				if (user == null)
				{
					return Result<ApplicationUserDto>.Failure(GetOperation, "User not found.");
				}

				var userDto = _mapper.Map<ApplicationUserDto>(user);
				return Result<ApplicationUserDto>.Success(userDto, OperationType.Get.ToString(), "User Retrieved successfully");
			}, OperationType.Get);

			
		}

		public async Task<Result<ApplicationUserFullProfileDto>> GetUserFullProfileAsync(string id)
		{
			return await _executor.Execute(async () =>
			{
				_logger.LogInformation("Retrieving full profile for user with ID {UserId}", id);
				if (string.IsNullOrWhiteSpace(id))
				{
					_logger.LogWarning("User ID must be provided.");
					return Result<ApplicationUserFullProfileDto>.Failure(GetOperation, "User ID must be provided.");
				}
				var user = await _userRepository.GetUserFullProfileAsync(id);
				if (user == null)
				{
					_logger.LogWarning("User with ID {UserId} not found.", id);
					return Result<ApplicationUserFullProfileDto>.Failure(GetOperation, "User not found.");
				}
				var userProfileDto = _mapper.Map<ApplicationUserFullProfileDto>(user);
				_logger.LogInformation("Retrieved full profile for user with ID {UserId}.", id);
				return Result<ApplicationUserFullProfileDto>.Success(userProfileDto, GetOperation, "Retrieved full profile for user .");
			}, OperationType.Get);
		}

		public async Task<Result<UserWithProjectsDto>> GetUserWithProjectsAsync(string id)
		{
			_logger.LogInformation("Retrieving user with projects for user ID {UserId}", id);
			return await _executor.Execute(async () =>
			{
				if (string.IsNullOrWhiteSpace(id))
				{
					_logger.LogWarning("User ID must be provided.");
					return Result<UserWithProjectsDto>.Failure(GetOperation, "User ID must be provided.");
				}
				var user = await _userRepository.GetByIdAsync(id);
				if (user == null)
				{
					_logger.LogWarning("User with ID {UserId} not found.", id);
					return Result<UserWithProjectsDto>.Failure(GetOperation, "User not found.");
				}
				var userWithProjectsDto = _mapper.Map<UserWithProjectsDto>(user);
				_logger.LogInformation("Retrieved user with projects for user ID {UserId}.", id);
				return Result<UserWithProjectsDto>.Success(userWithProjectsDto, GetOperation, "Retrieved user with projects");
			}, OperationType.Get);	
		}

		public async Task<Result<UserWithSkillsDto>> GetUserWithSkillsAsync(string id)
		{
			_logger.LogInformation("Retrieving user with skills for user ID {UserId}", id);
			return await _executor.Execute(async () =>
			{
				if (string.IsNullOrWhiteSpace(id))
				{
					_logger.LogWarning("User ID must be provided.");
					return Result<UserWithSkillsDto>.Failure(GetOperation, "User ID must be provided.");
				}
				var user = await _userRepository.GetByIdAsync(id);
				if (user == null)
				{
					_logger.LogWarning("User with ID {UserId} not found.", id);
					return Result<UserWithSkillsDto>.Failure(GetOperation, "User not found.");
				}
				var userWithSkillsDto = _mapper.Map<UserWithSkillsDto>(user);
				_logger.LogInformation("Retrieved user with skills for user ID {UserId}.", id);
				return Result<UserWithSkillsDto>.Success(userWithSkillsDto, GetOperation);
			}, OperationType.Get);
		}

		public async Task<Result<IEnumerable<ApplicationUserDto>>> SearchUsersByNameAsync(string name)
		{
			_logger.LogInformation("Searching users by name: {Name}", name);
			return await _executor.Execute(async () =>
			{
				if (string.IsNullOrWhiteSpace(name))
				{
					_logger.LogWarning("Search name was empty or whitespace.");
					return Result<IEnumerable<ApplicationUserDto>>.Success(Enumerable.Empty<ApplicationUserDto>(), GetOperation);
				}
				var allUsers = await _userRepository.GetAllAsync();
				var matchedUsers = allUsers.Where(u =>
					(!string.IsNullOrEmpty(u.FirstName) && u.FirstName.Contains(name, StringComparison.OrdinalIgnoreCase)) ||
					(!string.IsNullOrEmpty(u.LastName) && u.LastName.Contains(name, StringComparison.OrdinalIgnoreCase)) ||
					(!string.IsNullOrEmpty(u.UserName) && u.UserName.Contains(name, StringComparison.OrdinalIgnoreCase))
				).ToList();
				if (!matchedUsers.Any())
				{
					_logger.LogInformation("No users found matching search term '{SearchTerm}'.", name);
					return Result<IEnumerable<ApplicationUserDto>>.Success(Enumerable.Empty<ApplicationUserDto>(), GetOperation);
				}
				_logger.LogInformation("Found {Count} users matching search term '{SearchTerm}'.", matchedUsers.Count, name);
				var userDtos = _mapper.Map<IEnumerable<ApplicationUserDto>>(matchedUsers);
				return Result<IEnumerable<ApplicationUserDto>>.Success(userDtos, GetOperation);
			}, OperationType.Get);
			
		}
	}
}

