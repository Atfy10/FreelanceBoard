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
    public class ProposalRepository : BaseRepository<Proposal>, IProposalRepository
    {
        public ProposalRepository(AppDbContext dbContext) : base(dbContext) { }

        public async Task<List<Proposal>> GetByIdsAsync(IEnumerable<int> ids)
        {
            return await _dbContext.Proposals
                .Where(p => ids.Contains(p.Id))
                .ToListAsync();
        }

        public async Task<IEnumerable<Proposal>> GetProposalsByJobIdAsync(int jobId)
        {
            return await _dbContext.Proposals
                .Where(p => p.JobId == jobId)
                .Include(p => p.Freelancer) // include freelancer details for display
                .Include(p => p.Job) // optional: include job info if needed
                .ToListAsync();
        }
    }
}
