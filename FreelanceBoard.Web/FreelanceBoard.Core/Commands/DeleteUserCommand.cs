using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace FreelanceBoard.Core.Commands
{
	public class DeleteUserCommand : IRequest<bool>
	{
		public string UserId { get; private set; }
		public DeleteUserCommand(string userId)
		{
			if (string.IsNullOrWhiteSpace(userId))
			{
				throw new ArgumentException("UserId cannot be null or empty.", nameof(userId));
			}
			UserId = userId;
		}
	}
}
