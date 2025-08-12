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
            // From command → entity (ignore navigation props to prevent EF overwrite issues)
            CreateMap<CreateReviewCommand, Review>()
                .ForMember(dest => dest.Contract, opt => opt.Ignore())
                .ForMember(dest => dest.Reviewer, opt => opt.Ignore());

            // From entity → DTO
            CreateMap<Review, ReviewDto>()
                .ForMember(dest => dest.Contract, opt => opt.MapFrom(src => src.Contract))
                .ForMember(dest => dest.Reviewer, opt => opt.MapFrom(src => src.Reviewer));

            // Related entity → DTO
            CreateMap<Contract, ContractDto>()
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.PaymentNumber, opt => opt.MapFrom(src => src.PaymentNumber));

            CreateMap<ApplicationUser, ApplicationUserDto>();
        }
    }
}
