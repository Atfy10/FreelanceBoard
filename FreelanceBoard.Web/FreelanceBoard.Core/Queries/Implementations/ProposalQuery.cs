using AutoMapper;
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
                var proposals = await _proposalRepository.GetFullProposalWithIdAsync(id) ??
                    throw new KeyNotFoundException("No proposals found for the provided IDs.");

                var result = _mapper.Map<ProposalDto>(proposals);

                return Result<ProposalDto>.Success(result, GetOperation, "Proposals retrieved successfully.");
            }, OperationType.Get);
    }
}
