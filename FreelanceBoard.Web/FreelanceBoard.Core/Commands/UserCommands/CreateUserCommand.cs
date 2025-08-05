using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreelanceBoard.Core.Helpers;
using MediatR;

namespace FreelanceBoard.Core.Commands.UserCommands
{
	public class CreateUserCommand : IRequest<Result<string>>
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public string ConfirmPassword { get; set; }
		public string PhoneNumber { get; set; }
		public string UserName { get; set; }
	}
}
