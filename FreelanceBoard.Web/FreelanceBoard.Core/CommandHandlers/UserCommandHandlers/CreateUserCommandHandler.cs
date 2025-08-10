using AutoMapper;
using FreelanceBoard.Core.Commands.UserCommands;
using FreelanceBoard.Core.Domain.Entities;
using FreelanceBoard.Core.Domain.Enums;
using FreelanceBoard.Core.Dtos;
using FreelanceBoard.Core.Exceptions;
using FreelanceBoard.Core.Helpers;
using FreelanceBoard.Core.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Profile = FreelanceBoard.Core.Domain.Entities.Profile;

namespace FreelanceBoard.Core.CommandHandlers.UserCommandHandlers
{

	public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<string>>
	{
		private readonly IMapper _mapper;
		private readonly IUserRepository _userRepository;
        private readonly OperationExecutor _executor;
        private readonly IJwtToken _jwtToken;
        string AddOperation;
		private readonly IBaseRepository<Profile> _profileRepository;

		public CreateUserCommandHandler(IUserRepository userRepository,
			IMapper mapper, OperationExecutor executor, IJwtToken jwtToken,
			IBaseRepository<Profile> profileRepository)
		{
			_userRepository = userRepository;
			_mapper = mapper;
			_executor = executor;
			_jwtToken = jwtToken;
            AddOperation = OperationType.Add.ToString();
			_profileRepository = profileRepository;

        }

		public async Task<Result<string>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
			=> await _executor.Execute(async () =>
			{
				if (request == null)
					throw new NullReferenceException("Create request cannot be null.");

				var existingUser = await _userRepository.GetByEmailAsync(request.Email);

				if (existingUser != null)
					throw new EmailExistException("Email is already registered");

				var user = _mapper.Map<ApplicationUser>(request);

				var result = await _userRepository.CreateAsync(user, request.Password, request.Role);

				if (!result.Succeeded)
				{
					var errors = string.Join(", ", result.Errors.Select(e => e.Description));
					throw new InvalidOperationException($"User creation failed: {errors}");
				}

				var token = _jwtToken.GenerateJwtToken(user, request.Role);

				await _profileRepository.AddAsync(new Profile(){					
					UserId = user.Id,
					Bio = "",
					Image = ""
                });

                return Result<string>.Success(token, AddOperation,
					$"User with email {request.Email} created successfully.");
			}, OperationType.Add);
    }
}


