using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreelanceBoard.Core.Helpers;
using MediatR;

namespace FreelanceBoard.Core.Commands.UserCommands
{
	public class ChangePasswordCommand : IRequest<Result<string>>
	{
		public string UserId { get; set; }
		public string CurrentPassword { get; set; }
		public string NewPassword { get; set; }
		public string ConfirmNewPassword { get; set; }
	}
	
}
