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
        public JobRepository(AppDbContext dbContext) : base(dbContext)
        { }

        public async Task<Job?> GetFullJobWithIdAsync(int jobId)
        {
            return await _dbContext.Jobs
                         .Include(j => j.Skills)
                         .Include(j => j.Proposals)
                         .ThenInclude(p => p.Freelancer)
                         .FirstOrDefaultAsync(j => j.Id == jobId);
        }

        

    }
    
}
