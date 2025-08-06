using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreelanceBoard.Core.Domain.Entities;
using FreelanceBoard.Core.Interfaces;
using FreelanceBoard.Infrastructure.DBContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FreelanceBoard.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<ApplicationUser>, IUserRepository
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public UserRepository(AppDbContext context, UserManager<ApplicationUser> userManager) : base(context)
        {
            _context = context;
            _userManager = userManager;
        }

        public Task<ApplicationUser?> GetByEmailAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return Task.FromResult<ApplicationUser?>(null);

            return _userManager.FindByEmailAsync(email);
        }

        public async Task<ApplicationUser?> GetUserFullProfileAsync(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                return null;
            return await _dbContext.Users
                .Include(u => u.Profile)
                .Include(u => u.Skills)
                .Include(u => u.Projects)
                .FirstOrDefaultAsync(u => u.Id == userId);
        }

        public Task<bool> CheckPasswordAsync(ApplicationUser user, string password)
        {
            if (user == null || string.IsNullOrWhiteSpace(password))
                return Task.FromResult(false);

            return _userManager.CheckPasswordAsync(user, password);
        }

        public async Task<string> GetUserRolesAsync(ApplicationUser user)
        {
            if (user == null)
                return "";

            var roles = await _userManager.GetRolesAsync(user);
            return roles.ToList().FirstOrDefault();
        }

		public async Task<IdentityResult> CreateAsync(ApplicationUser user, string password, string role)
		{
			var result = await _userManager.CreateAsync(user, password);

			if (!result.Succeeded)
				return result;

			await _userManager.AddToRoleAsync(user, role);

			return result;
		}
	}

}
