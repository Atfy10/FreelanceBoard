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
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(100).WithMessage("Title cannot exceed 100 characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.");

            RuleFor(x => x.Category)
                .NotEmpty().WithMessage("Category is required.")
                .MaximumLength(50).WithMessage("Category cannot exceed 50 characters.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than zero.");

            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("User ID is required.");

            RuleFor(x => x.SkillNames)
                .NotEmpty().WithMessage("At least one skill is required.");

            RuleFor(x => x.Deadline)
                .GreaterThan(x => x.DateCreated.AddHours(12))
                .WithMessage("Deadline must be after the creation date with minimum 12 hours.");

            RuleFor(x => x.DateCreated)
                .Must(date => date.Date == DateTime.UtcNow.Date)
                .WithMessage("DateCreated must be today.");
        }
    }
}
