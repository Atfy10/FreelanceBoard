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
		Task<IEnumerable<ApplicationUser>> GetAllUsersAsync();
		Task<bool> IsUserBannedAsync(string id);
		Task<IEnumerable<ApplicationUser>> GetUsersByRoleAsync(string roleName);
		Task<IEnumerable<ApplicationUser>> SearchUsersAsync(string searchTerm);
		Task<int> GetTotalUsersCountAsync();
		Task<IEnumerable<ApplicationUser>> GetPaginatedUsersAsync(int pageNumber, int pageSize);

	}
}
