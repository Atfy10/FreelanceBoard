using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreelanceBoard.Core.Domain.Entities;

namespace FreelanceBoard.Core.Interfaces
{
	public interface IUserRepository : IBaseRepository<ApplicationUser>
	{
		Task<ApplicationUser?> GetUserFullProfileAsync(string userId);
		public Task AddAsync(ApplicationUser entity, string pawd);

	}
}
