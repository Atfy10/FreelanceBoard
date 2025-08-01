using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreelanceBoard.Core.Domain.Entities;
using FreelanceBoard.Core.Interfaces;
using FreelanceBoard.Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;

namespace FreelanceBoard.Infrastructure.Repositories
{
	public class UserRepository : BaseRepository<ApplicationUser>, IUserRepository
	{
		private readonly AppDbContext _context;

		public UserRepository(AppDbContext context) : base(context)
		{
			_context = context;
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
	}

}
