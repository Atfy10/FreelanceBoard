using FreelanceBoard.Core.Domain.Entities;
using FreelanceBoard.Core.Dtos.JobDtos;
using FreelanceBoard.Core.Helpers;
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
        Task<IEnumerable<Proposal>> GetProposalsByFreelancerIdAsync(string freelancerId);

    }
}
