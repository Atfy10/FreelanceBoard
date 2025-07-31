using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceBoard.Core.Domain.Entities
{
    public class Job
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public string UserId { get; set; }

        //  Navigation properties
        public virtual ApplicationUser User { get; set; }
        public virtual Contract Contract { get; set; }
        public virtual ICollection<Skill> Skills { get; set; } = [];
        public virtual ICollection<Proposal> Proposals { get; set; } = [];

    }
}
