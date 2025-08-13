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

        public async Task<List<Proposal>?> GetManyByIdsAsync(params int[] ids)
        {
            return await _dbContext.Proposals
                .Where(p => ids.Contains(p.JobId))
                .ToListAsync();
        }
        public async Task<Proposal?> GetFullProposalWithIdAsync(int id)
        {
            return await _dbContext.Proposals
                .Include(p => p.Job)
                .Include(p => p.Freelancer)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Proposal>> GetProposalsByJobIdAsync(int jobId)
        {
            return await _dbContext.Proposals
                .Where(p => p.JobId == jobId)
                .Include(p => p.Freelancer) // include freelancer details for display
                .Include(p => p.Job) // optional: include job info if needed
                .ToListAsync();
        }

        public async Task<IEnumerable<Proposal>> GetProposalsByFreelancerIdAsync(string freelancerId)
        {
            if (string.IsNullOrWhiteSpace(freelancerId))
                throw new ArgumentNullException(nameof(freelancerId), "Freelancer ID cannot be null or empty.");

            return await _dbContext.Proposals
                .Where(p => p.FreelancerId == freelancerId)
                .Include(p => p.Freelancer) // include freelancer details
                .Include(p => p.Job)        // include job details if needed
                .ToListAsync();
        }

    }
}
