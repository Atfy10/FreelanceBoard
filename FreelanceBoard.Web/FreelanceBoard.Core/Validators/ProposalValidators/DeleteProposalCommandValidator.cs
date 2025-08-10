using FluentValidation;
using FreelanceBoard.Core.Commands.ProposalCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceBoard.Core.Validators.ProposalValidators
{
    public class DeleteProposalCommandValidator : AbstractValidator<DeleteProposalCommand>
    {
        public DeleteProposalCommandValidator()
        {
            RuleFor(x => x.ProposalId).GreaterThan(0).NotEmpty().
                WithMessage("Proposal id must not be empty and greater than 0");
        }
    }
}
