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
    public class ContractRepository : BaseRepository<Job>,IContractRepository
    {
        public ContractRepository(AppDbContext dbContext) : base(dbContext)
        { }
        public async Task<Contract> GetFullContractWithIdAsync(int contractId)
        {
            return await _dbContext.Contracts
                          .Include(c => c.Job)
                          .Include(c => c.Reviews)
                          .Include(c => c.Payment)
                          .FirstOrDefaultAsync(c => c.Id == contractId);
        }
    }
}
