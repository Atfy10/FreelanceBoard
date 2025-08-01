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

namespace FreelanceBoard.Core.Queries.Implementations
{
	public class UserQuery : IUserQuery
	{
		private readonly IUserRepository _userRepository;
		private readonly IMapper _mapper;

		public UserQuery(IUserRepository userRepository, IMapper mapper)
		{
			_userRepository = userRepository;
			_mapper = mapper;
		}

		public async Task<IEnumerable<ApplicationUserDto>> GetAllBannedUsersAsync()
		{
			var allUsers = await _userRepository.GetAllAsync();
			var bannedUsers = allUsers.Where(u => u.IsBanned);
			var bannedUserDtos = _mapper.Map<IEnumerable<ApplicationUserDto>>(bannedUsers);
			return bannedUserDtos;
		}

		public async Task<IEnumerable<ApplicationUserDto>> GetAllUsersAsync()
		{
			var users = await _userRepository.GetAllAsync();
			var userDtos = _mapper.Map<IEnumerable<ApplicationUserDto>>(users);
			return userDtos;
		}


		public async Task<ApplicationUserDto> GetUserByIdAsync(string id)
		{
			var user = await _userRepository.GetByIdAsync(id);

			if (user == null)
				return null;

			return _mapper.Map<ApplicationUserDto>(user);
		}

		public async Task<ApplicationUserFullProfileDto> GetUserFullProfileAsync(string id)
		{
			var user = await _userRepository.GetUserFullProfileAsync(id);

			if (user == null)
				return null;

			return _mapper.Map<ApplicationUserFullProfileDto>(user);
		}

		public async Task<UserWithProjectsDto> GetUserWithProjectsAsync(string id)
		{
			var user = await _userRepository.GetByIdAsync(id);

			if (user == null)
				return null;

			return _mapper.Map<UserWithProjectsDto>(user);

		}

		public async Task<UserWithSkillsDto> GetUserWithSkillsAsync(string id)
		{
			var user = await _userRepository.GetByIdAsync(id);

			if (user == null)
				return null;

			return _mapper.Map<UserWithSkillsDto>(user);
		}

		public async Task<IEnumerable<ApplicationUserDto>> SearchUsersByNameAsync(string name)
		{
			if (string.IsNullOrWhiteSpace(name))
				return Enumerable.Empty<ApplicationUserDto>();

			var allUsers = await _userRepository.GetAllAsync();

			var matchedUsers = allUsers.Where(u =>
				(!string.IsNullOrEmpty(u.FirstName) && u.FirstName.Contains(name, StringComparison.OrdinalIgnoreCase)) ||
				(!string.IsNullOrEmpty(u.LastName) && u.LastName.Contains(name, StringComparison.OrdinalIgnoreCase)) ||
				(!string.IsNullOrEmpty(u.UserName) && u.UserName.Contains(name, StringComparison.OrdinalIgnoreCase))
			);

			var result = _mapper.Map<IEnumerable<ApplicationUserDto>>(matchedUsers);
			return result;
		}
	}
}
