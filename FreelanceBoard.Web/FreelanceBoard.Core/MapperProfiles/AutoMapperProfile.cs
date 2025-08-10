using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FreelanceBoard.Core.Commands.UserCommands;
using FreelanceBoard.Core.Dtos;

namespace FreelanceBoard.Core
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			CreateMap<CreateUserCommand, Domain.Entities.ApplicationUser>()
				.ForMember(dest => dest.Id, opt => opt.Ignore()) // auto-generated
				.ForMember(dest => dest.IsBanned, opt => opt.Ignore()); // default = false;
			CreateMap<UpdateUserCommand, Domain.Entities.ApplicationUser>();
			CreateMap<AddProjectCommand, Domain.Entities.Project>();
			CreateMap<Domain.Entities.ApplicationUser, ApplicationUserDto>();
			CreateMap<LoginCommand, LoginUserDto>();
			CreateMap<ApplicationUserDto, Domain.Entities.ApplicationUser>();
			CreateMap<UserUpdateDto, Domain.Entities.ApplicationUser>()
				.ForMember(dest => dest.Id, opt => opt.Ignore());
			CreateMap<Domain.Entities.ApplicationUser, UserWithProjectsDto>();
			CreateMap<Domain.Entities.ApplicationUser, UserWithSkillsDto>();
			CreateMap<Domain.Entities.Project, ProjectDto>();
			CreateMap<Domain.Entities.ApplicationUser, ApplicationUserFullProfileDto>();
			CreateMap<Domain.Entities.Profile, ProfileDto>();
			CreateMap<Domain.Entities.Skill, SkillDto>();
			CreateMap<AddSkillCommand, Domain.Entities.Skill>();
			CreateMap<AddUserSkillCommand, Domain.Entities.ApplicationUserSkill>();


		}
	}
}
