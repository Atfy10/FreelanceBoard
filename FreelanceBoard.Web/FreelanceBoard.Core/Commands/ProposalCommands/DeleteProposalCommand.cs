using FreelanceBoard.Core.Helpers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceBoard.Core.Commands.ProposalCommands
{
    public class DeleteProposalCommand : IRequest<Result<int>>
    {
        public int ProposalId { get; set; }
        public DeleteProposalCommand(int proposalId) => ProposalId = proposalId;
        
    }
}
