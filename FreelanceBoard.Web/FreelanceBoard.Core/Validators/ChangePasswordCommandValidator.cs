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
                .NotEmpty().WithMessage("User ID is required.");

            RuleFor(x => x.CurrentPassword)
                .NotEmpty().WithMessage("Current password is required.")
                .MinimumLength(6).WithMessage("Current password must be at least 6 characters long.")
                .Must(pwd => !string.IsNullOrWhiteSpace(pwd))
                    .WithMessage("Current password cannot contain only whitespace.");

            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("New password is required.")
                .MinimumLength(6).WithMessage("New password must be at least 6 characters long.")
                .NotEqual(x => x.CurrentPassword)
                    .WithMessage("New password must be different from the current password.")
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).+$")
                    .WithMessage("New password must contain at least one uppercase letter, one lowercase letter, one number, and one special character.");

            RuleFor(x => x.ConfirmNewPassword)
                .NotEmpty().WithMessage("Confirm new password is required.")
                .Equal(x => x.NewPassword)
                    .WithMessage("New password and confirm new password must match.");
        }
	}
}
