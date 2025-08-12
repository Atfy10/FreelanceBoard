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
            RuleFor(x => x.Message)
                .NotEmpty().WithMessage("Message is required.")
                .Must(m => !string.IsNullOrWhiteSpace(m))
                    .WithMessage("Message cannot be whitespace only.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than zero.");

            RuleFor(x => x.FreelancerId)
                .NotEmpty().WithMessage("Freelancer ID is required.");

            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Status is required.")
                .MaximumLength(10).WithMessage("Status cannot exceed 10 characters.");

            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Proposal ID is required.");
        }
    }
}
