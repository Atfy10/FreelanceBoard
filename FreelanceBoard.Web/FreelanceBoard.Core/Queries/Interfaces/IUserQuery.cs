using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreelanceBoard.Core.Domain.Entities;
using FreelanceBoard.Core.Dtos;

namespace FreelanceBoard.Core.Queries.Interfaces
{
	public interface IUserQuery
	{
		Task<ApplicationUserDto>GetUserByIdAsync(string id);
		Task<IEnumerable<ApplicationUserDto>> GetAllUsersAsync();
		Task<IEnumerable<ApplicationUserDto>> GetAllBannedUsersAsync();
		Task<IEnumerable<ApplicationUserDto>> SearchUsersByNameAsync(string name);
		Task<UserWithProjectsDto> GetUserWithProjectsAsync(string id);
		Task<UserWithSkillsDto> GetUserWithSkillsAsync(string id);
		Task<ApplicationUserFullProfileDto> GetUserFullProfileAsync(string id);




	}
}
