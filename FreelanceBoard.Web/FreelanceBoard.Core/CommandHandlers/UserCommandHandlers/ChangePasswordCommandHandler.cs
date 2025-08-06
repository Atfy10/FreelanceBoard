using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreelanceBoard.Core.Commands.UserCommands;
using FreelanceBoard.Core.Domain.Entities;
using FreelanceBoard.Core.Domain.Enums;
using FreelanceBoard.Core.Helpers;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace FreelanceBoard.Core.CommandHandlers.UserCommandHandlers
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Result<string>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly OperationExecutor _executor;
        private readonly string UpdateOperation = OperationType.Update.ToString();

        public ChangePasswordCommandHandler(UserManager<ApplicationUser> userManager,
            OperationExecutor executor)
        {
            _userManager = userManager;
            _executor = executor;
        }

        public async Task<Result<string>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
            => await _executor.Execute(async () =>
            {
                if (request == null)
                    throw new NullReferenceException("Change password request cannot be null.");

                var user = await _userManager.FindByIdAsync(request.UserId) ??
                           throw new KeyNotFoundException($"User with ID {request.UserId} not found.");

                var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword,
                    request.NewPassword);

                if (!result.Succeeded)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    throw new InvalidOperationException($"Password change failed: {errors}");
                }

                return Result<string>.Success(request.UserId, UpdateOperation,
                    $"Password for user {request.UserId} changed successfully.");
            }, OperationType.Update);
    }
}
