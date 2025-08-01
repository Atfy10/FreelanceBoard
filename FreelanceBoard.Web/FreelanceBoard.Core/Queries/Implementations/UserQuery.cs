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

		public async Task<IEnumerable<ApplicationUserDto>> GetAllUsersAsync()
		{
			var users = await _userRepository.GetAllAsync();
			var userDtos = _mapper.Map<IEnumerable<ApplicationUserDto>>(users);
			return userDtos;
		}

		public Task<IEnumerable<ApplicationUser>> GetPaginatedUsersAsync(int pageNumber, int pageSize)
		{
			throw new NotImplementedException();
		}

		public Task<int> GetTotalUsersCountAsync()
		{
			throw new NotImplementedException();
		}

		public Task<ApplicationUserDto> GetUserByIdAsync(string id)
		{
			var user = _userRepository.GetByIdAsync(id);
			if (user == null)
			{
				return Task.FromResult<ApplicationUserDto>(null);
			}
			var userDto = _mapper.Map<ApplicationUserDto>(user);
			return Task.FromResult(userDto);
		}

		public Task<IEnumerable<ApplicationUser>> GetUsersByRoleAsync(string roleName)
		{
			throw new NotImplementedException();
		}

		public Task<bool> IsUserBannedAsync(string id)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<ApplicationUser>> SearchUsersAsync(string searchTerm)
		{
			throw new NotImplementedException();
		}

		
	}
}
