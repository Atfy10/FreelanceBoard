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
    public class CreateFileCommand : IRequest<Result<bool>>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile File { get; set; }
        //public string UserId { get; set; }
    }
}
