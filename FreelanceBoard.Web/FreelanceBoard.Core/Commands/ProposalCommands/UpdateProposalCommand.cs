using FreelanceBoard.Core.Dtos.JobDtos;
using FreelanceBoard.Core.Helpers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceBoard.Core.Commands.ProposalCommands
{
    public class UpdateProposalCommand : IRequest<Result<ProposalDto>>
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }
        public decimal Price { get; set; }
        public string FreelancerId { get; set; }

        public UpdateProposalCommand(int id,string message, string status, decimal price, string freelancerId)
        {
            Id = id;
            Message = message;
            Status = status;
            Price = price;
            FreelancerId = freelancerId;

        }
    }
}
