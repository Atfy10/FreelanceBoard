using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FreelanceBoard.Core.Commands;
using FreelanceBoard.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FreelanceBoard.Core.CommandHandlers
{
	public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand,bool>
	{
		private readonly IUserRepository _userRepository;
		private readonly ILogger<UpdateUserCommandHandler> _logger;
		private readonly IMapper _mapper;

		public UpdateUserCommandHandler(IUserRepository userRepository, ILogger<UpdateUserCommandHandler> logger, IMapper mapper)
		{
			_userRepository = userRepository;
			_logger = logger;
			_mapper = mapper;
		}

		public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
		{
			var userDto = request.User;

			var existingUser = await _userRepository.GetByIdAsync(userDto.Id);
			if (existingUser is null)
			{
				_logger.LogWarning("User with ID {UserId} not found", userDto.Id);
				return false;
			}

			_mapper.Map(userDto, existingUser); 

			await _userRepository.UpdateAsync(existingUser);

			_logger.LogInformation("User with ID {UserId} updated successfully", userDto.Id);
			return true;
		}
	}
}
