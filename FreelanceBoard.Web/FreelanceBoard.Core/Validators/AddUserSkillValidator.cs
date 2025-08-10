using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FreelanceBoard.Core.Commands.UserCommands;

namespace FreelanceBoard.Core.Validators
{
	public class AddUserSkillValidator : AbstractValidator<AddUserSkillCommand>
	{
		public AddUserSkillValidator()
		{
			RuleFor(x => x.SkillName)
				.NotNull().WithMessage("Skill name cannot be null.")
				.NotEmpty().WithMessage("Skill name is required.")
				.MaximumLength(60).WithMessage("Skill name cannot exceed 60 characters.");
		}

	}
}
