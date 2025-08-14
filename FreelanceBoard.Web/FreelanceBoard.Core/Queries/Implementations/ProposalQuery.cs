using AutoMapper;
using FreelanceBoard.Core.Domain.Entities;
using FreelanceBoard.Core.Domain.Enums;
using FreelanceBoard.Core.Dtos.JobDtos;
using FreelanceBoard.Core.Helpers;
using FreelanceBoard.Core.Interfaces;
using FreelanceBoard.Core.Queries.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceBoard.Core.Queries.Implementations
{
    public class ProposalQuery : IProposalQuery
    {
        private readonly IProposalRepository _proposalRepository;
        private readonly IMapper _mapper;
        private readonly OperationExecutor _executor;
        private readonly string GetOperation;

        public ProposalQuery(IProposalRepository proposalRepository, IMapper mapper, OperationExecutor executor)
        {
            _proposalRepository = proposalRepository ?? throw new ArgumentNullException(nameof(proposalRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _executor = executor ?? throw new ArgumentNullException(nameof(executor));
            GetOperation = OperationType.Get.ToString();
        }
        public async Task<Result<ProposalDto>> GetProposalByIdAsync(int id)
            => await _executor.Execute(async () =>
            {
                if (id <= 0)
                    throw new ArgumentOutOfRangeException(nameof(id), "Proposal ID must be greater than zero.");

                var proposal = await _proposalRepository.GetFullProposalWithIdAsync(id) ??
                    throw new KeyNotFoundException("No proposal found for the provided ID.");

                var result = _mapper.Map<ProposalDto>(proposal);
                return Result<ProposalDto>.Success(result, GetOperation, "Proposal retrieved successfully.");
            }, OperationType.Get);
        public async Task<Result<IEnumerable<ProposalDto>>> GetProposalsByJobIdAsync(int jobId)
            => await _executor.Execute(async () =>
            {
                if (jobId <= 0)
                    throw new ArgumentOutOfRangeException(nameof(jobId), "Job ID must be greater than zero.");

                var proposals = await _proposalRepository.GetManyByIdsAsync(jobId);

                if (proposals == null || proposals.Count == 0)
                    return Result<IEnumerable<ProposalDto>>.Success([], GetOperation, "No proposals found for this job.");

                var result = _mapper.Map<IEnumerable<ProposalDto>>(proposals);
                return Result<IEnumerable<ProposalDto>>.Success(result, GetOperation, "Proposals retrieved successfully.");
            }, OperationType.Get);

        public async Task<Result<IEnumerable<ProposalDto>>> GetProposalsByFreelancerIdAsync(string freelancerId)
            => await _executor.Execute(async () =>
            {
                if (string.IsNullOrWhiteSpace(freelancerId))
                    throw new ArgumentNullException(nameof(freelancerId), "Freelancer ID cannot be null or empty.");

                var proposals = await _proposalRepository.GetProposalsByFreelancerIdAsync(freelancerId);

                if (proposals == null || !proposals.Any())
                    return Result<IEnumerable<ProposalDto>>.Success([], GetOperation, "No proposals found for this freelancer.");

                var result = _mapper.Map<IEnumerable<ProposalDto>>(proposals);
                return Result<IEnumerable<ProposalDto>>.Success(result, GetOperation, "Proposals retrieved successfully.");
            }, OperationType.Get);

    }
}
