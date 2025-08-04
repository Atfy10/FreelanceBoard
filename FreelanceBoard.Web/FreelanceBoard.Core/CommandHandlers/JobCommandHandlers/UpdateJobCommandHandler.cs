using AutoMapper;
using FreelanceBoard.Core.Commands.JobCommands;
using FreelanceBoard.Core.Domain.Entities;
using FreelanceBoard.Core.Dtos;
using FreelanceBoard.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceBoard.Core.CommandHandlers.JobHandlers
{
    public class UpdateJobCommandHandler : IRequestHandler<UpdateJobCommand, bool>
    {
        private readonly IJobRepository _jobRepository;
        private readonly IContractRepository _contractRepository;
        private readonly ISkillRepository _skillRepository;
        private readonly IProposalRepository _proposalRepository;
        private readonly IMapper _mapper;
        public UpdateJobCommandHandler(IJobRepository jobRepository, IContractRepository contractRepository,
            ISkillRepository skillRepository, IProposalRepository proposalRepository, IMapper mapper)
        {
            _jobRepository = jobRepository ?? throw new ArgumentNullException(nameof(jobRepository));
            _contractRepository = contractRepository ?? throw new ArgumentNullException(nameof(IContractRepository));
            _skillRepository = skillRepository ?? throw new ArgumentNullException(nameof(ISkillRepository));
            _proposalRepository = proposalRepository ?? throw new ArgumentNullException(nameof(IProposalRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<bool> Handle(UpdateJobCommand request, CancellationToken cancellationToken)
        {
            
            var existingJob = await _jobRepository.GetFullJobWithIdAsync(request.Id);
            if (existingJob == null)
                return false;


            Contract updatedContract = await _contractRepository.GetFullContractWithIdAsync(request.ContractId);
            if (updatedContract == null)
                return false;


            _mapper.Map(request, existingJob);
            existingJob.Contract = updatedContract;

            var updatedSkills = await _skillRepository.GetByNamesAsync(request.SkillNames);
            if (updatedSkills.Count != request.SkillNames.Count)
            {
                return false;
            }
            existingJob.Skills = updatedSkills;

            var updatedProposals = await _proposalRepository.GetByIdsAsync(request.ProposalIds);

            if (updatedProposals.Count != request.ProposalIds.Count)
            {
                return false;
            }
            existingJob.Proposals = updatedProposals;

            await _jobRepository.UpdateAsync(existingJob);
            return true;
        }
    }
}
