using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using FreelanceBoard.Core.Dtos;
using Microsoft.AspNetCore.Identity;

namespace FreelanceBoard.Core.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsBanned { get; set; }

        // Navigation properties 
        public virtual ICollection<Job> Jobs { get; set; } = [];
        public virtual Profile Profile { get; set; }
        public virtual ICollection<Proposal> Proposals { get; set; } = [];
        public virtual ICollection<Notification> Notifications { get; set; } = [];
        public virtual ICollection<Contract> Contracts { get; set; } = [];
        public virtual ICollection<Message> SentMessages { get; set; } = [];
        public virtual ICollection<Message> ReceivedMessages { get; set; } = [];
        public virtual ICollection<Project> Projects { get; set; } = [];
        public virtual ICollection<Skill> Skills { get; set; } = [];

    }

}
