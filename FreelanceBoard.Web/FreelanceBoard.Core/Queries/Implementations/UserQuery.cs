using AutoMapper;
using FreelanceBoard.Core.Domain.Entities;
using FreelanceBoard.Core.Domain.Enums;
using FreelanceBoard.Core.Dtos;
using FreelanceBoard.Core.Helpers;
using FreelanceBoard.Core.Interfaces;
using FreelanceBoard.Core.Queries.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FreelanceBoard.Core.Queries.Implementations
{
    public class UserQuery : IUserQuery
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly OperationExecutor _executor;

        private readonly string GetOperation;
        private readonly string GetALLOperation;


        public UserQuery(IUserRepository userRepository, IMapper mapper, OperationExecutor executor)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _executor = executor;
            GetOperation = OperationType.Get.ToString();
            GetALLOperation = OperationType.GetAll.ToString();
        }

        public async Task<Result<IEnumerable<ApplicationUserDto>>> GetAllBannedUsersAsync()
            => await _executor.Execute(async () =>
            {

                var allUsers = await _userRepository.GetAllAsync();
                var bannedUsers = allUsers.Where(u => u.IsBanned).ToList();

                if (bannedUsers.Count == 0)
                {
                    return Result<IEnumerable<ApplicationUserDto>>.
                    Success([], GetALLOperation, "No banned users found.");
                }



                var bannedUserDtos = _mapper.Map<IEnumerable<ApplicationUserDto>>(bannedUsers);

                return Result<IEnumerable<ApplicationUserDto>>
                .Success(bannedUserDtos, GetALLOperation, "Banned Users retreived successfully");
            }, OperationType.GetAll);
        public async Task<Result<IEnumerable<ApplicationUserDto>>> GetAllUsersAsync()
            => await _executor.Execute(async () =>
            {

                var allUsers = await _userRepository.GetAllAsync();
                if (!allUsers.Any())
                {
                    return Result<IEnumerable<ApplicationUserDto>>.Success(Enumerable.Empty<ApplicationUserDto>(), GetALLOperation, "No users found.");
                }
                var userDtos = _mapper.Map<IEnumerable<ApplicationUserDto>>(allUsers);
                return Result<IEnumerable<ApplicationUserDto>>.Success(userDtos, GetALLOperation, "Users retrieved successfully ");
            }, OperationType.GetAll);
        public async Task<Result<ApplicationUserDto>> GetUserByIdAsync(string id)
            => await _executor.Execute(async () =>
            {


                if (string.IsNullOrWhiteSpace(id))
                    throw new ArgumentNullException(nameof(id),
                        "User ID cannot be null or empty.");

                var user = await _userRepository.GetByIdAsync(id) ??
                    throw new KeyNotFoundException($"User with ID {id} not found.");

                var userDto = _mapper.Map<ApplicationUserDto>(user);

                return Result<ApplicationUserDto>.Success(userDto, OperationType.Get.ToString(),
                    "User Retrieved successfully");
            }, OperationType.Get);
        public async Task<Result<ApplicationUserFullProfileDto>> GetUserFullProfileAsync(string id)
            => await _executor.Execute(async () =>
            {

                if (string.IsNullOrWhiteSpace(id))
                    throw new ArgumentNullException(nameof(id),
                        "User ID cannot be null or empty.");

                var user = await _userRepository.GetUserFullProfileAsync(id) ??
                    throw new KeyNotFoundException($"User with ID {id} not found.");

                var userSkills = user.UserSkills?.Select(us => us.Skill).ToList() ?? [];



				var userProfileDto = _mapper.Map<ApplicationUserFullProfileDto>(user);

                var mappedUserSkill= _mapper.Map<List<SkillDto>>(userSkills);
				userProfileDto.Skills = mappedUserSkill;




                return Result<ApplicationUserFullProfileDto>.Success(userProfileDto,
                    GetOperation, "Retrieved full profile for user .");
            }, OperationType.Get);
        public async Task<Result<UserWithProjectsDto>> GetUserWithProjectsAsync(string id)
            => await _executor.Execute(async () =>
            {


                if (string.IsNullOrWhiteSpace(id))
                    throw new ArgumentNullException(nameof(id), "User ID must be provided.");

                var user = await _userRepository.GetByIdAsync(id) ??
                    throw new KeyNotFoundException($"User with ID {id} not found.");

                var userWithProjectsDto = _mapper.Map<UserWithProjectsDto>(user);

                if (userWithProjectsDto.Projects == null)
                {
                    userWithProjectsDto.Projects = [];
                }



                return Result<UserWithProjectsDto>.Success(userWithProjectsDto, GetOperation,
                    "Retrieved user with projects");
            }, OperationType.Get);
        public async Task<Result<UserWithSkillsDto>> GetUserWithSkillsAsync(string id)
            => await _executor.Execute(async () =>
            {

                if (string.IsNullOrWhiteSpace(id))
                    throw new ArgumentNullException(nameof(id), "User ID must be provided.");

                var user = await _userRepository.GetByIdAsync(id) ??
                    throw new KeyNotFoundException($"User with ID {id} not found.");

                var userWithSkillsDto = _mapper.Map<UserWithSkillsDto>(user);

                if (userWithSkillsDto.Skills == null)
                {

                    userWithSkillsDto.Skills = [];
                }



                return Result<UserWithSkillsDto>.Success(userWithSkillsDto, GetOperation);
            }, OperationType.Get);
        public async Task<Result<IEnumerable<ApplicationUserDto>>> SearchUsersByNameAsync(string name)
            => await _executor.Execute(async () =>
            {

                if (string.IsNullOrWhiteSpace(name))
                    throw new ArgumentNullException(nameof(name), "User name must be provided.");

                var allUsers = await _userRepository.GetAllAsync();

                var matchedUsers = allUsers.Where(u => IsExistingName(name, u)).ToList() ?? [];

                if (matchedUsers.Count == 0)
                {
                    return Result<IEnumerable<ApplicationUserDto>>
                    .Success(Enumerable.Empty<ApplicationUserDto>(), GetOperation);
                }


                var userDtos = _mapper.Map<IEnumerable<ApplicationUserDto>>(matchedUsers);

                return Result<IEnumerable<ApplicationUserDto>>.Success(userDtos, GetOperation);
            }, OperationType.Get);


        /*************Helper Methods**************/
        bool IsExistingName(string name, ApplicationUser u) =>
            (!string.IsNullOrEmpty(u.FirstName) && u.FirstName.Contains(name, StringComparison.OrdinalIgnoreCase)) ||
            (!string.IsNullOrEmpty(u.LastName) && u.LastName.Contains(name, StringComparison.OrdinalIgnoreCase)) ||
            (!string.IsNullOrEmpty(u.UserName) && u.UserName.Contains(name, StringComparison.OrdinalIgnoreCase));
    }
}

