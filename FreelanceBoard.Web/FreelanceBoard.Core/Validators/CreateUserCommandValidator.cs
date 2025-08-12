using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FreelanceBoard.Core.Commands.UserCommands;

namespace FreelanceBoard.Core.Validators
{
	public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
	{
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .Length(3, 50).WithMessage("First name must be between 3 and 50 characters.")
                .Must(name => !string.IsNullOrWhiteSpace(name))
                    .WithMessage("First name cannot contain only whitespace.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .Length(3, 50).WithMessage("Last name must be between 3 and 50 characters.")
                .Must(name => !string.IsNullOrWhiteSpace(name))
                    .WithMessage("Last name cannot contain only whitespace.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long.")
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).+$")
                    .WithMessage("Password must contain at least one uppercase letter, one lowercase letter, one number, and one special character.");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("Confirm password is required.")
                .Equal(x => x.Password).WithMessage("Passwords do not match.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required.")
                .Length(10, 15).WithMessage("Phone number must be between 10 and 15 characters.")
                .Matches(@"^\+?\d+$").WithMessage("Phone number must contain only digits (and optional leading +).");

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Username is required.")
                .Length(3, 20).WithMessage("Username must be between 3 and 20 characters.")
                .Matches(@"^[a-zA-Z0-9_]+$").WithMessage("Username can only contain letters, numbers, and underscores.");
        }
    }
}
