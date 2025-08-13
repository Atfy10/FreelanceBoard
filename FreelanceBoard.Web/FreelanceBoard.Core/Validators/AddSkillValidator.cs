using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FreelanceBoard.Core.Commands.UserCommands;

namespace FreelanceBoard.Core.Validators
{
	public class AddSkillValidator : AbstractValidator<AddSkillCommand>
	{
		public AddSkillValidator()
		{
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Skill name is required.")
                .MaximumLength(50).WithMessage("Skill name cannot exceed 50 characters.")
                .Must(BeAValidSkillName).WithMessage("Skill cannot fully be a number");
		}


        private bool BeAValidSkillName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return false;

            // Check if all characters are digits
            return !name.All(char.IsDigit);
        }
    }
}
