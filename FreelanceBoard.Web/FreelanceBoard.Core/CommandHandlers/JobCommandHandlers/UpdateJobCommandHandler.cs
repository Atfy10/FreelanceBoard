using AutoMapper;
using FreelanceBoard.Core.CommandHandlers.UserCommandHandlers;
using FreelanceBoard.Core.Commands.JobCommands;
using FreelanceBoard.Core.Domain.Entities;
using FreelanceBoard.Core.Domain.Enums;
using FreelanceBoard.Core.Dtos;
using FreelanceBoard.Core.Dtos.JobDtos;
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
    public class UpdateJobCommandHandler : IRequestHandler<UpdateJobCommand, Result<JobDto>>
    {
        private readonly IJobRepository _jobRepository;
        private readonly IContractRepository _contractRepository;
        private readonly ISkillRepository _skillRepository;
        private readonly IProposalRepository _proposalRepository;
        private readonly IMapper _mapper;
        private readonly OperationExecutor _executor;
        private readonly ILogger<UpdateJobCommandHandler> _logger;
        private readonly string UpdateOperation;
        public UpdateJobCommandHandler(IJobRepository jobRepository, IContractRepository contractRepository,OperationExecutor executor,
            ISkillRepository skillRepository, IProposalRepository proposalRepository, IMapper mapper,ILogger<UpdateJobCommandHandler> logger)
        {
            _jobRepository = jobRepository ?? throw new ArgumentNullException(nameof(jobRepository));
            _contractRepository = contractRepository ?? throw new ArgumentNullException(nameof(IContractRepository));
            _skillRepository = skillRepository ?? throw new ArgumentNullException(nameof(ISkillRepository));
            _proposalRepository = proposalRepository ?? throw new ArgumentNullException(nameof(IProposalRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _executor = executor ?? throw new ArgumentNullException(nameof(executor));
            UpdateOperation = OperationType.Update.ToString();
        }

        public async Task<Result<JobDto>> Handle(UpdateJobCommand request, CancellationToken cancellationToken)
        => await _executor.Execute(async () =>
        {
            _logger.LogInformation("Starting {Operation} process for Job ID: {JobId}", UpdateOperation, request.Id);
            var existingJob = await _jobRepository.GetFullJobWithIdAsync(request.Id);
            if (existingJob == null)
                throw new ArgumentNullException(nameof(request.Id), "Job ID was not found");


            Contract updatedContract = await _contractRepository.GetFullContractWithIdAsync(request.ContractId);
            if (updatedContract == null && request.ContractId!=0)
                throw new ArgumentNullException(nameof(request.ContractId), "Contract ID was not found");


            _mapper.Map(request, existingJob);
            existingJob.Contract = updatedContract;

            var updatedSkills = await _skillRepository.GetByNamesAsync(request.SkillNames);
            EnsureAllFound(request.SkillNames, updatedSkills.Count, "Skills");

            existingJob.Skills = updatedSkills;

            var updatedProposals = await _proposalRepository.GetByIdsAsync(request.ProposalIds);

            EnsureAllFound(request.ProposalIds, updatedProposals.Count, "Proposals");
            
            existingJob.Proposals = updatedProposals;
            var jobDto = _mapper.Map<JobDto>(existingJob);

            await _jobRepository.UpdateAsync(existingJob);
            return Result<JobDto>.Success(jobDto, UpdateOperation, "Job updated successfully.");
        }, OperationType.Update);


        private void EnsureAllFound<T>(List<T> foundItems, int expectedCount, string itemName)
        {
            if (foundItems.Count != expectedCount)
                throw new ArgumentNullException(nameof(foundItems), $"One or more {itemName} were not found");
        }
    }
}
