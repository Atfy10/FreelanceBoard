using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FreelanceBoard.Core.Commands;
using FreelanceBoard.Core.Domain.Entities;
using FreelanceBoard.Core.Services.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace FreelanceBoard.Core.CommandHandlers
{
	public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, string>
	{
		private readonly IUserService _userService;

		public CreateUserCommandHandler(IUserService userService)
		{
			_userService = userService;
		}

		public async Task<string> Handle(CreateUserCommand request, CancellationToken cancellationToken)
		{
			return await _userService.CreateUserAsync(request);
		}
	}
}
