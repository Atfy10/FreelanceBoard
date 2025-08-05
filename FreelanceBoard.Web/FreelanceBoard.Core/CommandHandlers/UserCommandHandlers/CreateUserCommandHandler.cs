using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FreelanceBoard.Core.Commands.UserCommands;
using FreelanceBoard.Core.Domain.Entities;
using FreelanceBoard.Core.Domain.Enums;
using FreelanceBoard.Core.Helpers;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace FreelanceBoard.Core.CommandHandlers.UserCommandHandlers
{

	public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<string>>
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IMapper _mapper;
		private readonly ILogger<CreateUserCommandHandler> _logger;
		private readonly OperationExecutor _executor;
		string AddOperation;

		public CreateUserCommandHandler(UserManager<ApplicationUser> userManager, IMapper mapper, ILogger<CreateUserCommandHandler> logger , OperationExecutor executor)
		{
			_userManager = userManager;
			_mapper = mapper;
			_logger = logger;
			_executor = executor;
			AddOperation = OperationType.Add.ToString();
		}

		public async Task<Result<string>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
		=> await _executor.Execute(async () =>
			{
				_logger.LogInformation("Starting user creation process...");
				if (request == null)
					throw new NullReferenceException("Create request cannot be null.");
				var existingUser = await _userManager.FindByEmailAsync(request.Email);
				if (existingUser != null)
				{
					return Result<string>.Failure("Email is already registered", AddOperation);
				}
				var user = _mapper.Map<ApplicationUser>(request);
				var result = await _userManager.CreateAsync(user, request.Password);
				if (!result.Succeeded)
				{
					var errors = string.Join(", ", result.Errors.Select(e => e.Description));
					throw new InvalidOperationException($"User creation failed: {errors}");
				}
				_logger.LogInformation("User with email {Email} created successfully.", request.Email);
				return Result<string>.Success(user.Id, AddOperation, "User created successfully.");
			}, OperationType.Add);
	}
}


