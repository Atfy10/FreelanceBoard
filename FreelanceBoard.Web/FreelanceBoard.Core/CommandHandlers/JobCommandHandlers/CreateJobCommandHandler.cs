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
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly OperationExecutor _executor;
        private readonly string CreateOperation;

        public CreateJobCommandHandler(IJobRepository jobRepo,
            ICategoryRepository categoryRepository,
            ISkillRepository skillRepository, IMapper mapper,
            OperationExecutor executor)
        {
            _jobRepository = jobRepo ?? throw new ArgumentNullException(nameof(jobRepo));
            _skillRepository = skillRepository ?? throw new ArgumentNullException(nameof(skillRepository));
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _executor = executor ?? throw new ArgumentNullException(nameof(executor));
            CreateOperation = OperationType.Add.ToString();

        }

        public async Task<Result<int>> Handle(CreateJobCommand request, CancellationToken cancellationToken)
            => await _executor.Execute(async () =>
            {
                if (request == null)
                    throw new ArgumentNullException(nameof(request), "CreateJobCommand request cannot be null.");

                var newJob = _mapper.Map<Job>(request);

                var skills = await _skillRepository.GetByNamesAsync(request.SkillNames);

                if (skills.Count != request.SkillNames.Count)
                    throw new ArgumentNullException(nameof(request), "One or more skills wasn't found");

                var categoryExists = await _categoryRepository.ExistsAsync(request.Category);

                if (!categoryExists)
                    throw new ArgumentNullException(nameof(request), "Category does not exist.");

                newJob.Skills = skills;

                await _jobRepository.AddAsync(newJob);

                await _categoryRepository.AssignCategoryToJob(newJob.Id, request.Category);


                return Result<int>.Success(newJob.Id, CreateOperation,
                    $"Job created successfully (ID: {newJob.Id}");
            
            }, OperationType.Add);
    }
}
