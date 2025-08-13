using FreelanceBoard.Core.Domain.Entities;
using FreelanceBoard.Core.Dtos.JobDtos;
using FreelanceBoard.Core.Helpers;
using FreelanceBoard.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceBoard.Core.Queries.Interfaces
{
    public interface IProposalQuery
    {
        Task<Result<ProposalDto>> GetProposalByIdAsync(int id);
        Task<Result<IEnumerable<ProposalDto>>> GetProposalsByJobIdAsync(int jobId);
        Task<Result<IEnumerable<ProposalDto>>> GetProposalsByFreelancerIdAsync(string freelancerId);

    }
}
