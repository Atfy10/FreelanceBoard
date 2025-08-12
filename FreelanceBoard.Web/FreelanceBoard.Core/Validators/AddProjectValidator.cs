using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FreelanceBoard.Core.CommandHandlers.UserCommandHandlers;
using FreelanceBoard.Core.Commands.UserCommands;

namespace FreelanceBoard.Core.Validators
{
	public class AddProjectValidator : AbstractValidator<AddProjectCommand>
	{
		public AddProjectValidator()
		{
			RuleFor(x => x.Title)
				.NotEmpty().WithMessage("Title is required.")
				.MaximumLength(100).WithMessage("Title cannot exceed 100 characters.");
			RuleFor(x => x.Description)
				.MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");
		}
		
	}
}
