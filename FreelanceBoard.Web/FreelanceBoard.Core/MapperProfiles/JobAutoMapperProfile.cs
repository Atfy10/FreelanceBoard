using AutoMapper;
using FreelanceBoard.Core.Commands.JobCommands;
using FreelanceBoard.Core.Domain.Entities;
using FreelanceBoard.Core.Dtos;
using FreelanceBoard.Core.Dtos.JobDtos;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Profile = AutoMapper.Profile;

namespace FreelanceBoard.Core.MapperProfiles
{
    public class JobAutoMapperProfile : Profile
    {
        public JobAutoMapperProfile()
        {
            // CreateJobCommand → Job
            CreateMap<CreateJobCommand, Job>()
                .ForMember(dest => dest.Skills, opt => opt.Ignore())
                .ForMember(dest => dest.Proposals, opt => opt.Ignore())
                .ForMember(dest => dest.Contract, opt => opt.Ignore())
                .ForMember(dest => dest.Categories, opt => opt.Ignore());

            // UpdateJobCommand → Job (Partial Update Support)
            CreateMap<UpdateJobCommand, Job>()
                .ForMember(dest => dest.Skills, opt => opt.Ignore())
                .ForMember(dest => dest.Proposals, opt => opt.Ignore())
                .ForMember(dest => dest.Contract, opt => opt.Ignore())
                .ForMember(dest => dest.Categories, opt => opt.Ignore())
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            // Job → JobDto
            CreateMap<Job, JobDto>()
                .ForMember(d => d.SkillNames, o => o.MapFrom(src => src.Skills.Select(s => s.Name)))
                .ForMember(d => d.Proposals, o => o.MapFrom(src => src.Proposals))
                .ForMember(d => d.Categories, o => o.MapFrom(src => src.Categories.Select(c => c.Name)));

            // Proposal → ProposalDto
            CreateMap<Proposal, ProposalDto>()
                .ForMember(d => d.FreelancerName, o => o.MapFrom(src => src.Freelancer.UserName));

            // Category → CategoryDto
            CreateMap<Category, CategoryDto>()
                .ForMember(d => d.Name, o => o.MapFrom(src => src.Name));
		}
	}
}
