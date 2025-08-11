using FreelanceBoard.Core.Helpers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceBoard.Core.Commands.ReviewCommands
{
    public class CreateReviewCommand : IRequest<Result<int>>
    {
        public int Rating { get; set; }
        public string Feedback { get; set; }
        public DateTime Date { get; set; }
        public int ContractId { get; set; }
        public string ReviewerId { get; set; }
    }
}
