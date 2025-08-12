using AutoMapper;
using FreelanceBoard.Core.Commands.UserCommands;
using FreelanceBoard.Core.Domain.Entities;
using FreelanceBoard.Core.Domain.Enums;
using FreelanceBoard.Core.Dtos;
using FreelanceBoard.Core.Helpers;
using FreelanceBoard.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceBoard.Core.CommandHandlers.UserCommandHandlers
{
    public class LoginCommandHandler(IUserRepository userRepository,
                        OperationExecutor executor,
                        IMapper mapper, IJwtToken jwtToken) : IRequestHandler<LoginCommand, Result<LoginUserDto>>
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly OperationExecutor _executor = executor;
        private readonly IMapper _mapper = mapper;
        private readonly IJwtToken _jwtToken = jwtToken;
        private readonly string LoginOperation = OperationType.Login.ToString();


        public async Task<Result<LoginUserDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
            => await _executor.Execute(async () =>
            {
                if (request == null)
                    throw new NullReferenceException("Login request cannot be null.");

                var user = await _userRepository.GetByEmailAsync(request.Email) ??
                    throw new KeyNotFoundException($"User with email {request.Email} not found.");

                var isValidPassword = await _userRepository.CheckPasswordAsync(user, request.Password);

                if (!isValidPassword)
                    return Result<LoginUserDto>.Failure(LoginOperation, "Invalid email or password.");

                var role = await _userRepository.GetUserRolesAsync(user) ??
                    throw new Exception("No roles found");

                var userDto = _mapper.Map<LoginUserDto>(request);


                userDto.Token = _jwtToken.GenerateJwtToken(user, role);

                return Result<LoginUserDto>.Success(userDto, LoginOperation, "Login successful.");
            }, OperationType.Login);

    }
}
