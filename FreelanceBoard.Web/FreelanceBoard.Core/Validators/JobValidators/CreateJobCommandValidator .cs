using FluentValidation;
using FreelanceBoard.Core.Commands.JobCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceBoard.Core.Validators.JobValidators
{
    public class CreateJobCommandValidator : AbstractValidator<CreateJobCommand>
    {
        public CreateJobCommandValidator()
        {
            RuleFor(x => x.Title).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.Category).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Price).GreaterThan(0).NotNull();
            RuleFor(x => x.UserId).NotEmpty().MaximumLength(100).NotNull();
            RuleFor(x => x.SkillNames).NotEmpty();
            RuleFor(x => x.Deadline)
                .GreaterThan(x=> x.DateCreated)
                .WithMessage("Deadline must be in the future.")
                .NotNull()
                .WithMessage("Deadline cannot be null.");
            RuleFor(x => x.DateCreated.Date)
                .Equal(DateTime.Now.Date)
                .WithMessage("DateCreated must be the same day you published.")
                .NotNull();
            RuleFor(x => x.Category)
                .NotEmpty()
                .WithMessage("Category cannot be empty.")
                .MaximumLength(50)
                .WithMessage("Category cannot exceed 50 characters.");
        }
    }
}
