using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceBoard.Core.Dtos
{
    public class UpdateProfilePictureDto
    {
        public string UserId { get; set; }

        [Required]
        public IFormFile ProfileImage { get; set; }
    }

}
