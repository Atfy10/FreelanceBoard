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
	public class ProfileRepository : BaseRepository<Profile>, IProfileRepository
	{
		public ProfileRepository(AppDbContext dbContext) : base(dbContext) { }

		public async Task<Profile> GetByUserIdAsync(string userId)
		{
			var profile = await _dbContext.Profiles
		.FirstOrDefaultAsync(p => p.UserId == userId);

			return profile;
		}

	}
}
