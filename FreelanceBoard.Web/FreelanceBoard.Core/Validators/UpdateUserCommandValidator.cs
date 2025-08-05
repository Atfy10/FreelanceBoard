using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FreelanceBoard.Core.Commands.UserCommands;

namespace FreelanceBoard.Core.Validators
{
	public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
	{
		public UpdateUserCommandValidator()
		{
			RuleFor(command => command.Id)
				.NotEmpty().WithMessage("User ID is required.");
			RuleFor(command => command.FirstName)
				.NotEmpty().WithMessage("First name is required.")
				.MaximumLength(50).WithMessage("First name cannot exceed 50 characters.");
			RuleFor(command => command.LastName)
				.NotEmpty().WithMessage("Last name is required.")
				.MaximumLength(50).WithMessage("Last name cannot exceed 50 characters.");
			RuleFor(command => command.Email)
				.NotEmpty().WithMessage("Email is required.")
				.EmailAddress().WithMessage("Invalid email format.");
			RuleFor(command => command.PhoneNumber)
				.NotEmpty().WithMessage("Phone number is required.")
				.MaximumLength(15).WithMessage("Phone number cannot exceed 15 characters.");
			RuleFor(command => command.UserName)
				.NotEmpty().WithMessage("Username is required.")
				.MaximumLength(20).WithMessage("Username cannot exceed 20 characters.");
			
			RuleFor(command => command.IsBanned)
				.Must(b => b == true || b == false).WithMessage("IsBanned must be a boolean value.");
		}

	}
}
