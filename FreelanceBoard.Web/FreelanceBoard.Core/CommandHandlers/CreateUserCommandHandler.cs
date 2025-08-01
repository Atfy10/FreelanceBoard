using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FreelanceBoard.Core.Commands;
using FreelanceBoard.Core.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace FreelanceBoard.Core.CommandHandlers
{
	public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, string>
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IMapper _mapper;
		private readonly ILogger<CreateUserCommandHandler> _logger;

		public CreateUserCommandHandler(UserManager<ApplicationUser> userManager, IMapper mapper, ILogger<CreateUserCommandHandler> logger)
		{
			_userManager = userManager;
			_mapper = mapper;
			_logger = logger;
		}

		public async Task<string> Handle(CreateUserCommand request, CancellationToken cancellationToken)
		{
			var user = _mapper.Map<ApplicationUser>(request);

			var result = await _userManager.CreateAsync(user, request.Password);

			if (!result.Succeeded)
			{
				var errors = string.Join(", ", result.Errors.Select(e => e.Description));
				_logger.LogError("Failed to create user: {Errors}", errors);
				throw new Exception($"Failed to create user: {errors}");
			}

			_logger.LogInformation("User created successfully with ID: {UserId}", user.Id);
			return user.Id;
		}
	}
}
