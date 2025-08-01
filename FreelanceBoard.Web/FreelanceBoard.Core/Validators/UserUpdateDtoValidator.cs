using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FreelanceBoard.Core.Dtos;

namespace FreelanceBoard.Core.Validators
{
	public class UserUpdateDtoValidator : AbstractValidator<UserUpdateDto>
	{
		public UserUpdateDtoValidator()
		{
			RuleFor(x => x.Id)
				.NotEmpty().WithMessage("User ID cannot be null or empty.")
				.NotNull().WithMessage("User ID cannot be null.");
			RuleFor(x => x.FirstName)
				.NotEmpty().WithMessage("First name is required.")
				.MaximumLength(50).WithMessage("First name cannot exceed 50 characters.");
			RuleFor(x => x.LastName)
				.NotEmpty().WithMessage("Last name is required.")
				.MaximumLength(50).WithMessage("Last name cannot exceed 50 characters.");
			RuleFor(x => x.Email)
				.NotEmpty().WithMessage("Email is required.")
				.EmailAddress().WithMessage("Invalid email format.");
			RuleFor(x => x.PhoneNumber)
				.NotEmpty().WithMessage("Phone number is required.")
				.MaximumLength(15).WithMessage("Phone number cannot exceed 15 characters.");
			RuleFor(x => x.UserName)
				.NotEmpty().WithMessage("Username is required.")
				.MaximumLength(20).WithMessage("Username cannot exceed 20 characters.");
			RuleFor(x => x.IsBanned)
				.Must(b => b == true || b == false).WithMessage("IsBanned must be a boolean value.");
		}
	}
}
