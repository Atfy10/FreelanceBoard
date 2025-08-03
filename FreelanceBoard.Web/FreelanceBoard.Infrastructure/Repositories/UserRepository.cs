using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreelanceBoard.Core.Domain.Entities;
using FreelanceBoard.Core.Interfaces;
using FreelanceBoard.Infrastructure.DBContext;
using Microsoft.AspNet.Identity;
using Microsoft.EntityFrameworkCore;

namespace FreelanceBoard.Infrastructure.Repositories
{
	public class UserRepository : BaseRepository<ApplicationUser>, IUserRepository
	{
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AppDbContext _context;
		//private readonly IBaseRepository<ApplicationUser> _baseRepo;
		public UserRepository(UserManager<ApplicationUser> userManager, AppDbContext context, IBaseRepository<ApplicationUser> baseRepo) : base(context)
		{

			_userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
			_context = context;
			//_baseRepo = baseRepo;
		}

        public Task AddAsync(ApplicationUser entity, string pawd)
        {
            throw new NotImplementedException();
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

		//public Task AddAsync(ApplicationUser user)
		//{
		//	return _baseRepo.AddAsync(user);
		//}
	}

}
