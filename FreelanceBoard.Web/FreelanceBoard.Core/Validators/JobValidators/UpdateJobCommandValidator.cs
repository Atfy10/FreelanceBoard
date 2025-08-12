using FluentValidation;
using FreelanceBoard.Core.Commands.JobCommands;
using Microsoft.IdentityModel.Tokens;
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
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Job ID is required.");

            RuleFor(x => x.Title)
                .NotEmpty().MaximumLength(100);

            RuleFor(x => x.Description)
                .NotEmpty();

            RuleFor(x => x.Category)
                .NotEmpty().WithMessage("Category cannot be empty.")
                .MaximumLength(50).WithMessage("Category cannot exceed 50 characters.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than zero.")
                .NotNull();

            RuleFor(x => x.UserId)
                .NotEmpty().MaximumLength(100);

            RuleFor(x => x.SkillNames)
                .NotEmpty().WithMessage("At least one skill is required.");

            RuleForEach(x => x.ProposalIds);

            RuleFor(x => x.ContractId);

            RuleFor(x => x.Deadline)
                .GreaterThan(DateTime.Now)
                .WithMessage("Deadline must be in the future.")
                .NotNull().WithMessage("Deadline cannot be null.");
        }
    }
}
