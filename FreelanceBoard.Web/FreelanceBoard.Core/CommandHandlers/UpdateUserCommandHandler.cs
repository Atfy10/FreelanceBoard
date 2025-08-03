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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace FreelanceBoard.Core.CommandHandlers
{
	public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand,bool>
	{
		private readonly IUserService _userService;

		public UpdateUserCommandHandler(IUserService userService)
		{
			_userService = userService;
		}

		public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
		{
			return await _userService.UpdateUserAsync(request);
		}
	}
}
