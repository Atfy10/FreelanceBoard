using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreelanceBoard.Core.Domain.Entities;
using FreelanceBoard.Core.Dtos;
using FreelanceBoard.Core.Helpers;

namespace FreelanceBoard.Core.Queries.Interfaces
{
	public interface IUserQuery
	{
		Task<Result<ApplicationUserDto>> GetUserByIdAsync(string id);
		Task<Result<IEnumerable<ApplicationUserDto>>> GetAllUsersAsync();
		Task<Result<IEnumerable<ApplicationUserDto>>> GetAllBannedUsersAsync();
		Task<Result<IEnumerable<ApplicationUserDto>>> SearchUsersByNameAsync(string name);
		Task<Result<UserWithProjectsDto>> GetUserWithProjectsAsync(string id);
		Task<Result<UserWithSkillsDto>> GetUserWithSkillsAsync(string id);
		Task<Result<ApplicationUserFullProfileDto>> GetUserFullProfileAsync(string id);




	}
}
