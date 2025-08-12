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
    public class JobRepository : BaseRepository<Job>, IJobRepository
    {

        public JobRepository(AppDbContext dbContext) : base(dbContext) { }

        public async Task<Job?> GetFullJobWithIdAsync(int jobId)
        {
            return await GetAllJobsWithDetails()
                .FirstOrDefaultAsync(j => j.Id == jobId);
        }
        public async Task<IEnumerable<Job>> GetAllJobsSortByBudget(bool isAscending = true)
        {
            return isAscending ?
                await GetAllJobsWithDetails()
                .OrderBy(j => j.Price)
                .ToListAsync()
                : await GetAllJobsWithDetails()
                .OrderByDescending(j => j.Price)
                .ToListAsync();
        }
        public async Task<IEnumerable<Job>> GetAllJobsSortByDate(bool isAscending = true)
        {
            return isAscending ?
                await GetAllJobsWithDetails()
                .OrderBy(j => j.DateCreated)
                .ToListAsync()
                : await GetAllJobsWithDetails()
                .OrderByDescending(j => j.DateCreated)
                .ToListAsync();
        }
        public async Task<IEnumerable<Job>?> GetJobsFilteredSkills(List<string> skills)
        {
            return await GetAllJobsWithDetails()
                .Where(s => s.Skills.Any(sk => skills.Contains(sk.Name))).ToListAsync();
        }
        public async Task<IEnumerable<Job>?> GetJobsFilteredCategory(List<string> categories)
        {
            return await GetAllJobsWithDetails()
                .Where(s => s.Categories.Any(sk => categories.Contains(sk.Name))).ToListAsync();
        }
        public async Task<IEnumerable<Job>?> GetJobsFilteredBudget(int min, int max)
        {
            return await GetAllJobsWithDetails()
                .Where(i => i.Price >= min && i.Price <= max)
                .ToListAsync();
        }

        private IQueryable<Job> GetAllJobsWithDetails()
        {
            return _dbContext.Jobs
                .Include(j => j.Skills)
                .Include(c => c.Categories)
                .Include(j => j.Proposals)
                .ThenInclude(p => p.Freelancer);
        }

        public async Task<IEnumerable<Job>?> GetJobsByUserIdAsync(string userId)
        {
            return await GetAllJobsWithDetails()
                .Where(j => j.UserId == userId)
                .ToListAsync();
        }

    }

}
