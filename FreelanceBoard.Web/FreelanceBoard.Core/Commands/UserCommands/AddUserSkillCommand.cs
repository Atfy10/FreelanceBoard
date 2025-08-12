using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreelanceBoard.Core.Helpers;
using MediatR;

namespace FreelanceBoard.Core.Commands.UserCommands
{
	public class AddUserSkillCommand : IRequest<Result<int>>
	{
		public string UserId { get; set; }
		public string SkillName { get; set; }

	}
}
