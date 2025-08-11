using FreelanceBoard.Core.Domain.Entities;
using FreelanceBoard.Core.Dtos.JobDtos;
using FreelanceBoard.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceBoard.Core.Queries.Interfaces
{
    public interface IProposalQuery
    {
        Task<Result<IEnumerable<ProposalDto>>> GetProposalsByJobIdAsync(int jobId);
    }
}
