using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreelanceBoard.Core.Helpers;
using MediatR;

namespace FreelanceBoard.Core.Commands.UserCommands
{
	public class UpdateUserProfileCommand : IRequest<Result<bool>>
	{
		public string PhoneNumber { get; set; }
		public string UserName { get; set; }

		public string Bio { get; set; }
	}

}
