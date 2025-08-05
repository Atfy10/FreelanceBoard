using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FreelanceBoard.Core.Commands.UserCommands;

namespace FreelanceBoard.Core.Validators
{
	internal class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
	{
		public ChangePasswordCommandValidator()
		{
			RuleFor(x => x.UserId)
				.NotEmpty().WithMessage("User ID is required.")
				.NotNull().WithMessage("User ID cannot be null.");
			RuleFor(x => x.CurrentPassword)
				.NotEmpty().WithMessage("Current password is required.")
				.MinimumLength(6).WithMessage("Current password must be at least 6 characters long.");
			RuleFor(x => x.NewPassword)
				.NotEmpty().WithMessage("New password is required.")
				.MinimumLength(6).WithMessage("New password must be at least 6 characters long.")
				.NotEqual(x => x.CurrentPassword).WithMessage("New password must be different from the current password.");
			RuleFor(x => x.ConfirmNewPassword)
				.NotEmpty().WithMessage("Confirm new password is required.")
				.Equal(x => x.NewPassword).WithMessage("New password and confirm new password must match.");
		}
	}
}
