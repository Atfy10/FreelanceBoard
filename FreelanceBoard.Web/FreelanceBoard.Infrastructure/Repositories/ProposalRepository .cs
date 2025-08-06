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
    }
}
