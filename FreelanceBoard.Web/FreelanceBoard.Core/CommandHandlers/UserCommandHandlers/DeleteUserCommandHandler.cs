using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FreelanceBoard.Core.Commands.UserCommands;
using FreelanceBoard.Core.Domain.Entities;
using FreelanceBoard.Core.Domain.Enums;
using FreelanceBoard.Core.Dtos;
using FreelanceBoard.Core.Helpers;
using FreelanceBoard.Core.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace FreelanceBoard.Core.CommandHandlers.UserCommandHandlers
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Result<string>>
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<CreateUserCommandHandler> _logger;
        private readonly OperationExecutor _executor;
        private readonly string DeleleOperation;


        public DeleteUserCommandHandler(IUserRepository userRepository, ILogger<CreateUserCommandHandler> logger, OperationExecutor executor)
        {
            _userRepository = userRepository;
            _logger = logger;
            _executor = executor;
            DeleleOperation = OperationType.Delete.ToString();
        }

        public async Task<Result<string>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        => await _executor.Execute(async () =>
            {
                _logger.LogInformation("Starting {Operation} process...", DeleleOperation);

                if (request == null)
                    throw new NullReferenceException("Update request cannot be null.");

                if (string.IsNullOrWhiteSpace(request.UserId))
                    throw new ArgumentNullException("User ID cannot be null or empty.");

                var user = await _userRepository.GetByIdAsync(request.UserId) ??
                throw new KeyNotFoundException($"User with ID {request.UserId} not found.");

                await _userRepository.DeleteAsync(request.UserId);

                _logger.LogInformation("User with ID {UserId} deleted successfully.", request.UserId);

                return Result<string>.Success("", DeleleOperation, "User deleted successfully.");
            }, OperationType.Delete);


    }
}
