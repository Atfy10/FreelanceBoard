using FreelanceBoard.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceBoard.Core.Interfaces
{
	public interface IUserRepository : IBaseRepository<ApplicationUser>
	{
		public Task<ApplicationUser?> GetByEmailAsync(string email);
        Task<ApplicationUser?> GetUserFullProfileAsync(string userId);
        public Task<bool> CheckPasswordAsync(ApplicationUser user, string password);
        public Task<string> GetUserRolesAsync(ApplicationUser user);




    }
}
