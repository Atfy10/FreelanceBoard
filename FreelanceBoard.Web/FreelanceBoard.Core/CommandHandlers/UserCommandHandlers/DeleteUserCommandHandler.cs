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
        private readonly OperationExecutor _executor;
        private readonly string DeleleOperation;


        public DeleteUserCommandHandler(IUserRepository userRepository, OperationExecutor executor)
        {
            _userRepository = userRepository;
            _executor = executor;
            DeleleOperation = OperationType.Delete.ToString();
        }

        public async Task<Result<string>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
            => await _executor.Execute(async () =>
            {
                if (request == null)
                    throw new NullReferenceException("Update request cannot be null.");

                var user = await _userRepository.GetByIdAsync(request.UserId) ??
                    throw new KeyNotFoundException($"User with ID {request.UserId} not found.");

                await _userRepository.DeleteAsync(request.UserId);

                return Result<string>.Success(user.Id, DeleleOperation,
                    $"User with ID {request.UserId} deleted successfully.");
            }, OperationType.Delete);

    }
}
