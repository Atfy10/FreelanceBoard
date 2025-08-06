using AutoMapper;
using FreelanceBoard.Core.Commands.JobCommands;
using FreelanceBoard.Core.Domain.Entities;
using FreelanceBoard.Core.Dtos;
using FreelanceBoard.Core.Dtos.JobDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Profile = AutoMapper.Profile;

namespace FreelanceBoard.Core.MapperProfiles
{
    public class JobAutoMapperProfile  : Profile
    {
        public JobAutoMapperProfile()
        {

            CreateMap<CreateJobCommand, Job>()
                .ForMember(dest => dest.Skills, opt => opt.Ignore())
                .ForMember(dest => dest.Proposals, opt => opt.Ignore())
                .ForMember(dest => dest.Contract, opt => opt.Ignore());

            CreateMap<UpdateJobCommand, Job>()
                .ForMember(dest => dest.Skills, opt => opt.Ignore())
                .ForMember(dest => dest.Proposals, opt => opt.Ignore())
                .ForMember(dest => dest.Contract, opt => opt.Ignore());

            CreateMap<Job, JobDto>()
            .ForMember(d => d.SkillNames,
                        o => o.MapFrom(src => src.Skills.Select(s => s.Name)))
            .ForMember(d => d.Proposals,
                        o => o.MapFrom(src => src.Proposals));

            CreateMap<Proposal, ProposalDto>()
                .ForMember(d => d.FreelancerName,
                            o => o.MapFrom(src => src.Freelancer.UserName));


        }
    }
}
