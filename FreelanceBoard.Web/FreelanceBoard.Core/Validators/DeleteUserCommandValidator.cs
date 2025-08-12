using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FreelanceBoard.Core.Commands.UserCommands;

namespace FreelanceBoard.Core.Validators
{
	internal class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
	{
        public DeleteUserCommandValidator()
        {
            RuleFor(command => command.UserId)
                .NotEmpty().WithMessage("User ID is required.");
        }
    }
}
