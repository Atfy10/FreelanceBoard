using FluentValidation;
using FreelanceBoard.Core.Commands.JobCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceBoard.Core.Validators.JobValidators
{
    public class UpdateJobCommandValidator : AbstractValidator<UpdateJobCommand>
    {
        public UpdateJobCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
            RuleFor(x => x.Title).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.Category).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Price).GreaterThan(0).NotNull();
            RuleFor(x => x.UserId).NotEmpty().MaximumLength(100).NotNull();
            RuleFor(x => x.SkillNames);
            RuleFor(x => x.ProposalIds);
            RuleFor(x => x.ContractId).GreaterThan(-1);
        }
    }
}
