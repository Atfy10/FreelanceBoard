using FreelanceBoard.Core.Domain.Entities;
using FreelanceBoard.Core.Interfaces;
using FreelanceBoard.Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceBoard.Infrastructure.Repositories
{
    public class SkillRepository : BaseRepository<Skill>, ISkillRepository
    {
        public SkillRepository(AppDbContext dbContext) : base(dbContext) { }

        public async Task<List<Skill>> GetByNamesAsync(IEnumerable<string> names)
        {
            return await _dbContext.Skills
                                   .Where(s => names.Contains(s.Name))
                                   .ToListAsync();
        }

		public async Task<int?> GetIdByNameAsync(string name)
		{
			return await _dbContext.Skills
				.Where(s => s.Name == name)
				.Select(s => (int?)s.Id)
				.FirstOrDefaultAsync();
		}

		public async Task<ApplicationUserSkill?> GetUserSkillAsync(string userId, int skillId)
		{
			return await _dbContext.Set<ApplicationUserSkill>()
			.FirstOrDefaultAsync(us =>
			us.ApplicationUsersId == userId &&
			us.SkillsId == skillId);

		}
	}
}
