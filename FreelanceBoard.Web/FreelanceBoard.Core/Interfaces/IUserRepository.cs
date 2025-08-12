using FreelanceBoard.Core.Domain.Entities;
using Microsoft.AspNetCore.Identity;
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

        /// <summary>
        /// Create user and assign a role to it.
        /// </summary>
        /// <param name="user">user without id</param>
        /// <param name="pwd"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public Task<IdentityResult> CreateAsync(ApplicationUser user, string pwd, string role);

		Task<bool> UsernameExistsAsync(string username, string excludeUserId = null);
		Task<bool> PhoneNumberExistsAsync(string phoneNumber, string excludeUserId = null);


	}
}
