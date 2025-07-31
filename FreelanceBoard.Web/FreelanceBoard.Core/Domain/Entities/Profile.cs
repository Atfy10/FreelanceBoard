using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceBoard.Core.Domain.Entities
{
    public class Profile
    {
        [Key]
        public int Id { get; set; }
        public string Bio { get; set; }
        public string Image { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }

        // Navigation properties
        public virtual ApplicationUser User { get; set; }
    }
}
