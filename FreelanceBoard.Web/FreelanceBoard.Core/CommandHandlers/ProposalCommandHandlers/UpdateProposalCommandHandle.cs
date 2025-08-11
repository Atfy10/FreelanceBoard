using AutoMapper;
using FreelanceBoard.Core.Commands.ProposalCommands;
using FreelanceBoard.Core.Domain.Entities;
using FreelanceBoard.Core.Domain.Enums;
using FreelanceBoard.Core.Dtos.JobDtos;
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
    internal class UpdateProposalCommandHandle : IRequestHandler<UpdateProposalCommand, Result<ProposalDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IProposalRepository _proposalRepository;
        private readonly IMapper _mapper;
        private readonly OperationExecutor _executor;
        private readonly string CreateOperation;

        public UpdateProposalCommandHandle(IUserRepository userRepository,IProposalRepository proposalRepository, IMapper mapper, OperationExecutor executor)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _proposalRepository = proposalRepository ?? throw new ArgumentNullException(nameof(proposalRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _executor = executor ?? throw new ArgumentNullException(nameof(executor));
            CreateOperation = OperationType.Update.ToString();
        }

        public async Task<Result<ProposalDto>> Handle(UpdateProposalCommand request, CancellationToken cancellationToken)
        => await _executor.Execute(async () =>
        {

            if (request == null)
                throw new NullReferenceException("Delete request cannot be null.");

            if (request.Id <= 0)
                throw new ArgumentOutOfRangeException("Proposal Id must be greater than zero.");

            var proposal = await _proposalRepository.GetByIdAsync(request.Id) ??
                throw new KeyNotFoundException("Proposal with the provided ID was not found.");

            var updatedProposal = _mapper.Map(request, proposal);

            //
            updatedProposal.Freelancer = await _userRepository.GetByIdAsync(request.FreelancerId) ??
                throw new KeyNotFoundException("Freelancer with the provided ID was not found.");

            await _proposalRepository.UpdateAsync(updatedProposal);

            var result = _mapper.Map<ProposalDto>(updatedProposal);
            return Result<ProposalDto>.Success(result, CreateOperation, "Proposal updated successfully.");

        },OperationType.Update);
    }
}
