using AutoMapper;
using FreelanceBoard.Core.Domain.Enums;
using FreelanceBoard.Core.Dtos.JobDtos;
using FreelanceBoard.Core.Helpers;
using FreelanceBoard.Core.Interfaces;
using FreelanceBoard.Core.Queries.Implementations;
using FreelanceBoard.Core.Queries.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceBoard.Core.QueryHandlers.JobQueryHandlers
{
    public class JobQuery : IJobQuery
    {
        private readonly IJobRepository _jobRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<JobQuery> _logger;
        private readonly OperationExecutor _executor;
        private readonly string GetOperation;
        public JobQuery(IJobRepository jobRepo, IMapper mapper,ILogger<JobQuery> logger, OperationExecutor executor)
        {
            _jobRepository = jobRepo;
            _mapper = mapper;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _executor = executor ?? throw new ArgumentNullException(nameof(executor));
            GetOperation = OperationType.Get.ToString();

        }


        public async Task<Result<JobDto>> GetJobByIdAsync(int id)
        => await _executor.Execute(async () =>
        {
            _logger.LogInformation("Getting job by job id");
            var job = await _jobRepository.GetFullJobWithIdAsync(id);
            if (job == null)
                throw new ArgumentNullException(nameof(id),"Job ID was not found");

            var result = _mapper.Map<JobDto>(job);

            _logger.LogInformation("Job with ID {JobId} retrieved successfully.", id);
            return Result<JobDto>.Success(result, GetOperation, "Job retrieved successfully.");
        }, OperationType.Get);
        
        
    }
}
