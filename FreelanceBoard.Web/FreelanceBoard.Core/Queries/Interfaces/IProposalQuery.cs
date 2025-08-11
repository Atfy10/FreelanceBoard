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
    }
}
