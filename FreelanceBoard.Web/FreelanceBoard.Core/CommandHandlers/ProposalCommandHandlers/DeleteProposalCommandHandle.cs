using AutoMapper;
using FreelanceBoard.Core.Commands.ProposalCommands;
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
    internal class DeleteProposalCommandHandle : IRequestHandler<DeleteProposalCommand, Result<int>>
    {
        private readonly IProposalRepository _proposalRepository;
        private readonly OperationExecutor _executor;
        private readonly string CreateOperation;

        public DeleteProposalCommandHandle(
            IProposalRepository proposalRepository,
            OperationExecutor executor)
        {
            _proposalRepository = proposalRepository;
            _executor = executor;
            CreateOperation = OperationType.Delete.ToString();
        }
        public async Task<Result<int>> Handle(DeleteProposalCommand request, CancellationToken cancellationToken)
        => await _executor.Execute(async () =>
        {
            var proposal = await _proposalRepository.GetByIdAsync(request.ProposalId) ??
                throw new KeyNotFoundException("Proposal with the provided ID was not found.");

            await _proposalRepository.DeleteAsync(proposal.Id);
            return Result<int>.Success(proposal.Id, CreateOperation, "Proposal deleted successfully.");
        },OperationType.Delete);
    }
}
