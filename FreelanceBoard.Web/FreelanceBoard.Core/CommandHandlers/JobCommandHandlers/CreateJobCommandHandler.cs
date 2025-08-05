using AutoMapper;
using FreelanceBoard.Core.CommandHandlers.UserCommandHandlers;
using FreelanceBoard.Core.Commands.JobCommands;
using FreelanceBoard.Core.Domain.Entities;
using FreelanceBoard.Core.Dtos;
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
    public class CreateJobCommandHandler : IRequestHandler<CreateJobCommand, int>
    {
        private readonly IJobRepository _jobRepository;
        private readonly ISkillRepository _skillRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateJobCommandHandler> _logger;

        public CreateJobCommandHandler(IJobRepository jobRepo, ISkillRepository skillRepository, IMapper mapper, ILogger<CreateJobCommandHandler> logger)
        {
            _jobRepository = jobRepo ?? throw new ArgumentNullException(nameof(jobRepo));
            _skillRepository = skillRepository ?? throw new ArgumentNullException(nameof(skillRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        public async Task<int> Handle(CreateJobCommand request, CancellationToken cancellationToken)
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
            return newJob.Id;

        }
    }
}
