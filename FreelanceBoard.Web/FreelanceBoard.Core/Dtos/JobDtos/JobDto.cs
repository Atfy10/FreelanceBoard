using FreelanceBoard.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceBoard.Core.Dtos.JobDtos
{
    public class JobDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string UserId { get; set; }

        public DateTime Deadline { get; set; }
        public DateTime DateCreated { get; set; }
        public List<string> Categories { get; set; } = [];
        public List<string> SkillNames { get; set; } = [];
        public List<ProposalDto> Proposals { get; set; } = [];

    }
}
