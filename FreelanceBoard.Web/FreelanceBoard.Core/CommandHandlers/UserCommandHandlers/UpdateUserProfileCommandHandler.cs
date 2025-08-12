using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreelanceBoard.Core.Commands.UserCommands;
using FreelanceBoard.Core.Domain.Enums;
using FreelanceBoard.Core.Dtos;
using FreelanceBoard.Core.Helpers;
using FreelanceBoard.Core.Interfaces;
using MediatR;

namespace FreelanceBoard.Core.CommandHandlers.UserCommandHandlers
{
	public class UpdateUserProfileCommandHandler : IRequestHandler<UpdateUserProfileCommand, Result<bool>>
	{
		private readonly IUserRepository _userRepository;
		private readonly IProfileRepository _profileRepository;
		private readonly IUserAccessor _userAccessor;
		private readonly OperationExecutor _executor;
		private readonly string UpdateOperation;

		public UpdateUserProfileCommandHandler(
			IUserRepository userRepository,
			IProfileRepository profileRepository,
			IUserAccessor userAccessor,
			OperationExecutor executor)
		{
			_userRepository = userRepository;
			_profileRepository = profileRepository;
			_userAccessor = userAccessor;
			_executor = executor;
			UpdateOperation = OperationType.Update.ToString();

		}

		public Task<Result<bool>> Handle(UpdateUserProfileCommand request, CancellationToken cancellationToken)
		=> _executor.Execute(async () =>
		{
			var userId = _userAccessor.GetUserId();
			if (string.IsNullOrEmpty(userId))
				throw new UnauthorizedAccessException("User is not authenticated.");
			var user = await _userRepository.GetByIdAsync(userId) ??
					 throw new KeyNotFoundException($"User with ID {userId} not found.");
			if (await _userRepository.UsernameExistsAsync(request.UserName, userId))
				throw new InvalidOperationException($"Username {request.UserName} is already taken.");
			if (await _userRepository.PhoneNumberExistsAsync(request.PhoneNumber, userId))
				throw new InvalidOperationException($"Phone number {request.PhoneNumber} is already in use.");
			user.PhoneNumber = request.PhoneNumber;
			user.UserName = request.UserName;
			var profile = await _profileRepository.GetByIdAsync(userId) ??
					 throw new KeyNotFoundException($"Profile not found.");

			profile.Bio = request.Bio;
			await _userRepository.UpdateAsync(user);
			await _profileRepository.UpdateAsync(profile);
			return Result<bool>.Success(true, UpdateOperation,
					$"Profile of User with ID {user.Id} updated successfully.");
		},OperationType.Update);
	}
}
