using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FreelanceBoard.Core.Domain.Entities;
using FreelanceBoard.Core.Dtos;
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

		public UserQuery(IUserRepository userRepository, IMapper mapper, ILogger<UserQuery> logger)
		{
			_userRepository = userRepository;
			_mapper = mapper;
			_logger = logger;
		}

		public async Task<IEnumerable<ApplicationUserDto>> GetAllBannedUsersAsync()
		{
			var allUsers = await _userRepository.GetAllAsync();
			var bannedUsers = allUsers.Where(u => u.IsBanned);
			_logger.LogInformation("Retrieved {Count} banned users.", bannedUsers.Count());

			return _mapper.Map<IEnumerable<ApplicationUserDto>>(bannedUsers);
		}

		public async Task<IEnumerable<ApplicationUserDto>> GetAllUsersAsync()
		{
			var users = await _userRepository.GetAllAsync();
			_logger.LogInformation("Retrieved all users. Count: {Count}", users.Count());

			return _mapper.Map<IEnumerable<ApplicationUserDto>>(users);
		}

		public async Task<ApplicationUserDto?> GetUserByIdAsync(string id)
		{
			var user = await _userRepository.GetByIdAsync(id);

			if (user == null)
			{
				_logger.LogWarning("User with ID {UserId} not found.", id);
				return null;
			}

			_logger.LogInformation("Retrieved user with ID {UserId}.", id);
			return _mapper.Map<ApplicationUserDto>(user);
		}

		public async Task<ApplicationUserFullProfileDto?> GetUserFullProfileAsync(string id)
		{
			var user = await _userRepository.GetUserFullProfileAsync(id);

			if (user == null)
			{
				_logger.LogWarning("Full profile for user ID {UserId} not found.", id);
				return null;
			}

			_logger.LogInformation("Retrieved full profile for user ID {UserId}.", id);
			return _mapper.Map<ApplicationUserFullProfileDto>(user);
		}

		public async Task<UserWithProjectsDto?> GetUserWithProjectsAsync(string id)
		{
			var user = await _userRepository.GetByIdAsync(id);

			if (user == null)
			{
				_logger.LogWarning("User with ID {UserId} not found when getting projects.", id);
				return null;
			}

			_logger.LogInformation("Retrieved user with projects for user ID {UserId}.", id);
			return _mapper.Map<UserWithProjectsDto>(user);
		}

		public async Task<UserWithSkillsDto?> GetUserWithSkillsAsync(string id)
		{
			var user = await _userRepository.GetByIdAsync(id);

			if (user == null)
			{
				_logger.LogWarning("User with ID {UserId} not found when getting skills.", id);
				return null;
			}

			_logger.LogInformation("Retrieved user with skills for user ID {UserId}.", id);
			return _mapper.Map<UserWithSkillsDto>(user);
		}

		public async Task<IEnumerable<ApplicationUserDto>> SearchUsersByNameAsync(string name)
		{
			if (string.IsNullOrWhiteSpace(name))
			{
				_logger.LogWarning("Search name was empty or whitespace.");
				return Enumerable.Empty<ApplicationUserDto>();
			}

			var allUsers = await _userRepository.GetAllAsync();

			var matchedUsers = allUsers.Where(u =>
				(!string.IsNullOrEmpty(u.FirstName) && u.FirstName.Contains(name, StringComparison.OrdinalIgnoreCase)) ||
				(!string.IsNullOrEmpty(u.LastName) && u.LastName.Contains(name, StringComparison.OrdinalIgnoreCase)) ||
				(!string.IsNullOrEmpty(u.UserName) && u.UserName.Contains(name, StringComparison.OrdinalIgnoreCase))
			);

			_logger.LogInformation("Found {Count} users matching search term '{SearchTerm}'.", matchedUsers.Count(), name);

			return _mapper.Map<IEnumerable<ApplicationUserDto>>(matchedUsers);
		}
	}
}

