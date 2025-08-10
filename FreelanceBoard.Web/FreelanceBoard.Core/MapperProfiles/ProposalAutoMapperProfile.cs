using AutoMapper;
using FreelanceBoard.Core.Commands.ProposalCommands;
using FreelanceBoard.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Profile = AutoMapper.Profile;

namespace FreelanceBoard.Core.MapperProfiles
{
    public class ProposalAutoMapperProfile : Profile
    {
        public ProposalAutoMapperProfile() 
        {
            CreateMap<CreateProposalCommand, Proposal>()
                .ForMember(dest => dest.Freelancer, opt => opt.Ignore())
                .ForMember(dest => dest.Job, opt => opt.Ignore());



            CreateMap<UpdateProposalCommand, Proposal>()
                .ForMember(dest => dest.Freelancer, opt => opt.Ignore())
                .ForMember(dest => dest.Job, opt => opt.Ignore())
                .ForMember(dest => dest.JobId, opt => opt.Ignore());

            CreateMap<DeleteProposalCommand, Proposal>();
        }
    }
}
