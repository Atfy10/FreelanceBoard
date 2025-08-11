using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FreelanceBoard.Core.Commands.UserCommands;

namespace FreelanceBoard.Core.Validators
{
	internal class UpdateUserProfileValidator : AbstractValidator<UpdateUserProfileCommand>
	{
		public UpdateUserProfileValidator()
		{
			RuleFor(command => command.PhoneNumber)
				.NotEmpty().WithMessage("Phone number is required.")
				.MaximumLength(15).WithMessage("Phone number cannot exceed 15 characters.");
			RuleFor(command => command.UserName)
				.NotEmpty().WithMessage("Username is required.")
				.MaximumLength(20).WithMessage("Username cannot exceed 20 characters.");
			RuleFor(command => command.Bio)
				.MaximumLength(500).WithMessage("Bio cannot exceed 500 characters.");


		}
	}

}
