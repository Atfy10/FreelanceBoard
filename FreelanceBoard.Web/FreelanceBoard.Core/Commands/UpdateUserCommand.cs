using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreelanceBoard.Core.Dtos;
using MediatR;

namespace FreelanceBoard.Core.Commands
{
	public class UpdateUserCommand : IRequest<bool>
	{
		public UserUpdateDto User { get; set; }

		public UpdateUserCommand(UserUpdateDto user)
		{
			User = user;
		}
	}
}
