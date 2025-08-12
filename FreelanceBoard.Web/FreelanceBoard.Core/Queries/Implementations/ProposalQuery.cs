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

        public async Task<Result<IEnumerable<ProposalDto>>> GetProposalsByJobIdAsync(int jobId)
            => await _executor.Execute(async () =>
            {
                var proposals = await _proposalRepository.GetFullProposalWithIdAsync(id) ??
                    throw new KeyNotFoundException("No proposals found for the provided ID.");
                if (jobId <= 0)
                    throw new ArgumentOutOfRangeException(nameof(jobId), "Job ID must be greater than zero.");

                var proposals = await _proposalRepository.GetProposalsByJobIdAsync(jobId);

                var result = _mapper.Map<ProposalDto>(proposals);
                if (proposals == null || !proposals.Any())
                    return Result<IEnumerable<ProposalDto>>.Success([], GetOperation, "No proposals found for this job.");

                return Result<ProposalDto>.Success(result, GetOperation, "Proposals retrieved successfully.");
                var result = _mapper.Map<IEnumerable<ProposalDto>>(proposals);
                return Result<IEnumerable<ProposalDto>>.Success(result, GetOperation, "Proposals retrieved successfully.");
            }, OperationType.Get);
    }
}
