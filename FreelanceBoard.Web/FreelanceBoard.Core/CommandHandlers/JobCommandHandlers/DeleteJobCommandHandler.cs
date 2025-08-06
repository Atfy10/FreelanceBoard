using FreelanceBoard.Core.CommandHandlers.UserCommandHandlers;
using FreelanceBoard.Core.Commands.JobCommands;
using FreelanceBoard.Core.Domain.Entities;
using FreelanceBoard.Core.Domain.Enums;
using FreelanceBoard.Core.Helpers;
using FreelanceBoard.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceBoard.Core.CommandHandlers.JobHandlers
{
    public class DeleteJobCommandHandler : IRequestHandler<DeleteJobCommand, Result<int>>
    {
        private readonly IJobRepository _jobRepository;
        private readonly ILogger<DeleteJobCommand> _logger;
        private readonly OperationExecutor _executor;
        private readonly string DeleteOperation;
        public DeleteJobCommandHandler(IJobRepository jobRepo, ILogger<DeleteJobCommand> logger,OperationExecutor executor)
        {
            _jobRepository = jobRepo;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _executor = executor ?? throw new ArgumentNullException(nameof(executor));
            DeleteOperation = OperationType.Delete.ToString();
        }

        public async Task<Result<int>> Handle(DeleteJobCommand request, CancellationToken cancellationToken)
            => await _executor.Execute(async() =>
            {
                _logger.LogInformation("Starting {Operation} process...", DeleteOperation);
                if (request == null)
                    throw new NullReferenceException("Delete request cannot be null.");

                if (request.JobId <= 0)
                    throw new ArgumentOutOfRangeException("Job ID must be greater than zero.");

                var job = await _jobRepository.GetByIdAsync(request.JobId) ??
                    throw new ArgumentNullException(nameof(request.JobId), "Job ID was not found");

                await _jobRepository.DeleteAsync(request.JobId);
                _logger.LogInformation("Job with ID {JobId} deleted successfully.", request.JobId);
                return Result<int>.Success(job.Id, DeleteOperation, "Job deleted successfully.");
            }, OperationType.Delete);
        
    }
}
