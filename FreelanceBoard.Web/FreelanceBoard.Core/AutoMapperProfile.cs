using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace FreelanceBoard.Core
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			CreateMap<Commands.CreateUserCommand, Domain.Entities.ApplicationUser>(); 
		}
	}
}
