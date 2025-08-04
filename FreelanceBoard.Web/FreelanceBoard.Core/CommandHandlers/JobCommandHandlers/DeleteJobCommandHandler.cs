using FreelanceBoard.Core.Commands.JobCommands;
using FreelanceBoard.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceBoard.Core.CommandHandlers.JobHandlers
{
    public class DeleteJobCommandHandler : IRequestHandler<DeleteJobCommand, bool>
    {
        private readonly IJobRepository _jobRepository;

        public DeleteJobCommandHandler(IJobRepository jobRepo)
            => _jobRepository = jobRepo;

        public async Task<bool> Handle(DeleteJobCommand request, CancellationToken cancellationToken)
        {
            var existing = await _jobRepository.GetByIdAsync(request.JobId);
            if (existing == null)
                return false;

            await _jobRepository.DeleteAsync(request.JobId);
            return true;
        }
    }
}
