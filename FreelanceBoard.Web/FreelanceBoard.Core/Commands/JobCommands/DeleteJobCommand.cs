using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceBoard.Core.Commands.JobCommands
{
    public class DeleteJobCommand : IRequest<bool>
    {
        public int JobId { get; set; }
        public DeleteJobCommand(int jobId) => JobId = jobId;
    }
}
