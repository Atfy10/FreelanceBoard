using FreelanceBoard.Core.Domain.Entities;
using FreelanceBoard.Core.Dtos;
using FreelanceBoard.Core.Helpers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceBoard.Core.Commands.UserCommands
{
    public class LoginCommand : IRequest<Result<LoginUserDto>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
