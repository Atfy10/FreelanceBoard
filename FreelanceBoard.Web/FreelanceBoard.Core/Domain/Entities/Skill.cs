using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceBoard.Core.Domain.Entities
{
    public class Skill
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }

        // Navigation properties
        public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; } = [];
        public virtual ICollection<Job> Jobs { get; set; } = [];
    }
}
