using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreelanceBoard.Core.Helpers;
using MediatR;

namespace FreelanceBoard.Core.Commands.UserCommands
{
	public class AddProjectCommand : IRequest<Result<int>>
	{
		public string Title { get; set; }
		public string Description { get; set; }
		public string Attachments { get; set; }
	}
}
