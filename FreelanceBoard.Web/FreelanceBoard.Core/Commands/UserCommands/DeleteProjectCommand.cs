using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreelanceBoard.Core.Helpers;
using MediatR;

namespace FreelanceBoard.Core.Commands.UserCommands
{
	public class DeleteProjectCommand : IRequest<Result<bool>>
	{
		public int ProjectId { get; set; }
	}
}
