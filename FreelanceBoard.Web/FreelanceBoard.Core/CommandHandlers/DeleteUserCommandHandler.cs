using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FreelanceBoard.Core.Commands;
using FreelanceBoard.Core.Domain.Entities;
using FreelanceBoard.Core.Interfaces;
using FreelanceBoard.Core.Services.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FreelanceBoard.Core.CommandHandlers
{
	public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
	{
		private readonly IUserService _userService;

		public DeleteUserCommandHandler(IUserService userService)
		{
			_userService = userService;
		}

		public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
		{
			return await _userService.DeleteUserAsync(request);
		}

	}
}
