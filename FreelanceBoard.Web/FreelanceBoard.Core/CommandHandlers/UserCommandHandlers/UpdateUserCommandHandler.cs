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
using FreelanceBoard.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace FreelanceBoard.Core.CommandHandlers.UserCommandHandlers
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Result<ApplicationUser>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly OperationExecutor _executor;
        private readonly ILogger<UpdateUserCommandHandler> _logger;
        private readonly string UpdateOperation;

        public UpdateUserCommandHandler(IUserRepository userRepository, IMapper mapper, ILogger<UpdateUserCommandHandler> logger, OperationExecutor executor)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
            _executor = executor;
            UpdateOperation = OperationType.Update.ToString();
        }
        public async Task<Result<ApplicationUser>> Handle(UpdateUserCommand request,
            CancellationToken cancellationToken)
             => await _executor.Execute(async () =>
             {
                 _logger.LogInformation("Starting {Operation} process...", UpdateOperation);

                 if (request == null)
                     throw new NullReferenceException("Update request cannot be null.");

                 if (string.IsNullOrWhiteSpace(request.Id))
                     throw new ArgumentNullException("User ID cannot be null or empty.");

                 var user = await _userRepository.GetByIdAsync(request.Id) ??
                 throw new KeyNotFoundException($"User with ID {request.Id} not found.");

                 _mapper.Map(request, user);

                 await _userRepository.UpdateAsync(user);

                 _logger.LogInformation("User with ID {UserId} updated successfully.", request.Id);
                 return Result<ApplicationUser>.Success(user, UpdateOperation, "User updated successfully.");

             }, OperationType.Update);
    }

}
