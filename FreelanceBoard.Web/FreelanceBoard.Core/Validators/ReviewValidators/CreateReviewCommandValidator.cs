using FluentValidation;
using FreelanceBoard.Core.Commands.ReviewCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceBoard.Core.Validators.ReviewValidators
{
    public class CreateReviewCommandValidator : AbstractValidator<CreateReviewCommand>
    {
        public CreateReviewCommandValidator()
        {
            RuleFor(x => x.Rating)
                .InclusiveBetween(1, 5)
                .WithMessage("Rating must be between 1 and 5.");

            RuleFor(x => x.Feedback)
                .NotEmpty().WithMessage("Feedback cannot be empty.")
                .Must(f => !string.IsNullOrWhiteSpace(f))
                    .WithMessage("Feedback cannot be whitespace only.")
                .MaximumLength(500).WithMessage("Feedback cannot exceed 500 characters.");

            RuleFor(x => x.Date)
                .Must(date => date.Date == DateTime.UtcNow.Date)
                    .WithMessage("Date must be today's date.");

            RuleFor(x => x.ContractId)
                .GreaterThan(0).WithMessage("Contract ID must be greater than zero.");

            RuleFor(x => x.ReviewerId)
                .NotEmpty().WithMessage("Reviewer ID cannot be empty.");
        }
    }
}
