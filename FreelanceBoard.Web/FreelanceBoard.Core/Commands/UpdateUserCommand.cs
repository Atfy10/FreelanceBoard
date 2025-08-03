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
		public string Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string PhoneNumber { get; set; }
		public string UserName { get; set; }
		public bool IsBanned { get; set; }
	}
}
