using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceBoard.Core.Domain
{
    public class ApplicationUser : IdentityUser
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public bool IsBanned { get; set; }

        //Navigation Properties
        public virtual ICollection<Job> Jobs { get; set; }
        public virtual Profile Profile { get; set; }
        public virtual Proposal Proposal { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<Contract> Contracts { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<Skill> Skills { get; set; }

    }
}
