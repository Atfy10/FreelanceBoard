using FreelanceBoard.Core.Domain.Entities;
using FreelanceBoard.Core.Helpers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceBoard.Core.Commands.ProposalCommands
{
    public class CreateProposalCommand : IRequest<Result<int>>
    {   public int JobId { get; set; }

        public int FreelancerId { get; set; }

        public string Message { get; set; }

        public decimal Price { get; set; }

        public string Status { get; set; }

    }
}
