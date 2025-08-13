using AutoMapper;
using FreelanceBoard.Core.Commands.ProposalCommands;
using FreelanceBoard.Core.Domain.Entities;
using FreelanceBoard.Core.Domain.Enums;
using FreelanceBoard.Core.Helpers;
using FreelanceBoard.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceBoard.Core.CommandHandlers.ProposalCommandHandlers
{
    internal class CreateProposalCommandHandler : IRequestHandler<CreateProposalCommand, Result<int>>
    {

        private readonly IProposalRepository _proposalRepository;
        private readonly IJobRepository _jobRepository;
		private readonly IMapper _mapper;
        private readonly OperationExecutor _executor;
        private readonly string CreateOperation;

        public CreateProposalCommandHandler(IProposalRepository proposalRepository, IMapper mapper, OperationExecutor executor, IJobRepository jobRepository)
        {
            _proposalRepository = proposalRepository ?? throw new ArgumentNullException(nameof(proposalRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _executor = executor ?? throw new ArgumentNullException(nameof(executor));
            CreateOperation = OperationType.Add.ToString();
            _jobRepository = jobRepository ?? throw new ArgumentNullException(nameof(jobRepository));
		}
        public async Task<Result<int>> Handle(CreateProposalCommand request, CancellationToken cancellationToken)
        => await _executor.Execute(async () =>
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request), "CreateProposalCommand request cannot be null.");


            var userExist = await _proposalRepository.UserExistsAsync(request.FreelancerId);
            if (!userExist)
                return Result<int>.Failure(CreateOperation,
                $"Freelancer with ID `{request.FreelancerId}` not found");

            var jobExist = await _jobRepository.JobExistsAsync(request.JobId);
            if (!jobExist)
                return Result<int>.Failure(CreateOperation,
                $"Job with ID `{request.JobId}` not found");

            var existingProposal = await _proposalRepository.GetProposalByJobIdAndFreelancerIdAsync(request.JobId, request.FreelancerId);
            if (existingProposal)
                return Result<int>.Failure(CreateOperation,
                "you has already submitted a proposal for this job");


			var newProposal = _mapper.Map<Proposal>(request);


			await _proposalRepository.AddAsync(newProposal);
            return Result<int>.Success(newProposal.Id, CreateOperation, "Proposal created successfully.");
        }, OperationType.Add);
        
    }
}
