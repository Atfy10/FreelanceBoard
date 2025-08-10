using FreelanceBoard.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceBoard.Core.Interfaces
{
    public interface IProposalRepository : IBaseRepository<Proposal>
    {
        Task<List<Proposal>?> GetManyByIdsAsync(params int[] ids);

        Task<Proposal?> GetFullProposalWithIdAsync(int id);
    }
}
