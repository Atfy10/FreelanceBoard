using AutoMapper;
using FreelanceBoard.Core.CommandHandlers.UserCommandHandlers;
using FreelanceBoard.Core.Commands.JobCommands;
using FreelanceBoard.Core.Domain.Entities;
using FreelanceBoard.Core.Domain.Enums;
using FreelanceBoard.Core.Dtos;
using FreelanceBoard.Core.Helpers;
using FreelanceBoard.Core.Interfaces;
using FreelanceBoard.Core.Validators.JobValidators;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceBoard.Core.CommandHandlers.JobHandlers
{
    public class CreateJobCommandHandler : IRequestHandler<CreateJobCommand, Result<int>>
    {
        private readonly IJobRepository _jobRepository;
        private readonly ISkillRepository _skillRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateJobCommandHandler> _logger;
        private readonly OperationExecutor _executor;
        private readonly string CreateOperation;

        public CreateJobCommandHandler(IJobRepository jobRepo, ISkillRepository skillRepository, IMapper mapper, ILogger<CreateJobCommandHandler> logger,
            OperationExecutor executor)
        {
            _jobRepository = jobRepo ?? throw new ArgumentNullException(nameof(jobRepo));
            _skillRepository = skillRepository ?? throw new ArgumentNullException(nameof(skillRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _executor = executor ?? throw new ArgumentNullException(nameof(executor));
            CreateOperation = OperationType.Get.ToString();

        }


        public async Task<Result<int>> Handle(CreateJobCommand request, CancellationToken cancellationToken)
        => await _executor.Execute(async() =>
        {
            _logger.LogInformation("Handling CreateJobCommand for request: {@Request}", request);
            if (request == null)
            {
                _logger.LogError("CreateJobCommand request is null.");
                throw new ArgumentNullException(nameof(request), "CreateJobCommand request cannot be null.");
            }
            var newJob = _mapper.Map<Job>(request);
            var skills = await _skillRepository.GetByNamesAsync(request.SkillNames);
            newJob.Skills = skills;


            if (skills.Count != request.SkillNames.Count)
            {
                throw new ArgumentNullException(nameof(request), "One or more skills wasn't found");
            }
            _logger.LogInformation("Created new job with title: {JobTitle} successfully", newJob.Title);
            await _jobRepository.AddAsync(newJob);
            return Result<int>.Success(newJob.Id, CreateOperation, "Job created successfully.");
        },OperationType.Get);
    }
}
