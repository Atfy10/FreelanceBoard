using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FreelanceBoard.Core.Commands;

namespace FreelanceBoard.Core.Validators
{
	public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
	{
		public UpdateUserCommandValidator()
		{
			RuleFor(command => command.User)
				.NotNull().WithMessage("User cannot be null.")
				.SetValidator(new UserUpdateDtoValidator());
		}

	}
}
