using FluentValidation;
using FreelanceBoard.Core.Commands.ProposalCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace FreelanceBoard.Core.Validators.ProposalValidators
{
    public class UpdateProposalCommandValidator : AbstractValidator<UpdateProposalCommand>
    {
        public UpdateProposalCommandValidator()
        {
            RuleFor(x => x.Message).NotEmpty();
            RuleFor(x => x.Price).GreaterThan(0);
            RuleFor(x => x.FreelancerId)
                .NotEmpty().WithMessage("Freelancer id must not be empty");

            RuleFor(x => x.Status)
                .NotEmpty()
                .MaximumLength(10).WithMessage("Status has to be less than 10 characters");

            RuleFor(x => x.Id)
                .GreaterThan(0)
                .NotEmpty().WithMessage("Proposal id must not be empty and greater than 0");

        }
    }
}
