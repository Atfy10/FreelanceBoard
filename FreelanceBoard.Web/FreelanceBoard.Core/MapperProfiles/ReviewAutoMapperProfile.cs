using AutoMapper;
using FreelanceBoard.Core.Commands.ReviewCommands;
using FreelanceBoard.Core.Domain.Entities;
using FreelanceBoard.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Profile = AutoMapper.Profile;
namespace FreelanceBoard.Core.MapperProfiles
{
    public class ReviewAutoMapperProfile : Profile
    {
        public ReviewAutoMapperProfile()
        {
            CreateMap<CreateReviewCommand, Review>()
                .ForMember(dest => dest.Contract, opt => opt.Ignore())
                .ForMember(dest => dest.Reviewer, opt => opt.Ignore());

            CreateMap<Review, ReviewDto>()
                .ForMember(dest => dest.Contract, src => src.MapFrom(r => r.Contract))
                .ForMember(dest => dest.Reviewer, src => src.MapFrom(r => r.Reviewer));

            CreateMap<Contract, ContractDto>()
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(c => c.StartDate))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(c => c.EndDate))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(c => c.Price))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(c => c.Status))
                .ForMember(dest => dest.PaymentNumber, opt => opt.MapFrom(c => c.PaymentNumber));

            CreateMap<ApplicationUser, ApplicationUserDto>();
        }
    }
}
