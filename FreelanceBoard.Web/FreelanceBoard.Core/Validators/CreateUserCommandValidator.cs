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
				.Length(2, 50).WithMessage("First name must be between 2 and 50 characters.");
			RuleFor(x => x.LastName)
				.NotEmpty().WithMessage("Last name is required.")
				.Length(2, 50).WithMessage("Last name must be between 2 and 50 characters.");
			RuleFor(x => x.Email)
				.NotEmpty().WithMessage("Email is required.")
				.NotNull().WithMessage("Email cannot be null.")
				.EmailAddress().WithMessage("Invalid email format.");
			RuleFor(x => x.Password)
				.NotEmpty().WithMessage("Password is required.")
				.NotNull().WithMessage("Email cannot be null.")
				.MinimumLength(6).WithMessage("Password must be at least 6 characters long.");
			RuleFor(x => x.ConfirmPassword)
				.Equal(x => x.Password).WithMessage("Passwords do not match.");
			RuleFor(x => x.PhoneNumber)
				.NotEmpty().WithMessage("Phone number is required.")
				.Length(10, 15).WithMessage("Phone number must be between 10 and 15 characters.");
			RuleFor(x => x.UserName)
				.NotEmpty().WithMessage("Username is required.")
				.Length(3, 20).WithMessage("Username must be between 3 and 20 characters.");
		}
	}
}
