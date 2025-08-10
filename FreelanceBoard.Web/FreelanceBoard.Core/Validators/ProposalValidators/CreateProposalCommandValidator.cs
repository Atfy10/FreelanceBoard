using FluentValidation;
using FreelanceBoard.Core.Commands.ProposalCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceBoard.Core.Validators.ProposalValidators
{
    public class CreateProposalCommandValidator : AbstractValidator<CreateProposalCommand>
    {
        public CreateProposalCommandValidator() 
        {
            RuleFor(x => x.Message).NotEmpty();
            RuleFor(x => x.Price).GreaterThan(0);
            RuleFor(x => x.FreelancerId).NotEmpty().WithMessage("FreeLancer id must not be empty").
                GreaterThan(0).WithMessage("Id must be greater than 0");
            RuleFor(x => x.Status).NotEmpty().MaximumLength(30).WithMessage("Status has to be less than 30 characters");
            RuleFor(x => x.JobId).NotEmpty().WithMessage("Job id must not be empty").
                GreaterThan(0).WithMessage("Id mus be greater than 0");
        }
    }
}
