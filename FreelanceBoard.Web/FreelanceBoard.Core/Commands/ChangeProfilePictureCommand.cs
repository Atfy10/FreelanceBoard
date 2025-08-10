using FreelanceBoard.Core.Helpers;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceBoard.Core.Commands
{
    public class ChangeProfilePictureCommand : IRequest<Result<string>>
    {
        public IFormFile ProfilePicture { get; set; }
    }


}
