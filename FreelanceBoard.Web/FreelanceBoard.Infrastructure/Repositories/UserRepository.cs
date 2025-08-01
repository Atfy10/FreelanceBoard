using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreelanceBoard.Core.Interfaces;

using FreelanceBoard.Core.Domain.Entities;
using FreelanceBoard.Infrastructure.DBContext;

namespace FreelanceBoard.Infrastructure.Repositories
{
	internal class UserRepository : BaseRepository<ApplicationUser>, IUserRepository
	{
		private readonly AppDbContext _context;

		public UserRepository(AppDbContext context) : base(context)
		{
			_context = context;
		}

		public Task<ApplicationUser> GetUserWithDetails(string id)
		{
			throw new NotImplementedException();
		}
	}

}
