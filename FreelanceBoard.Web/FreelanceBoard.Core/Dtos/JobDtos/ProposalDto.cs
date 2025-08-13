using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceBoard.Core.Dtos.JobDtos
{
    public class ProposalDto
    {
        public int Id { get; set; }

        public int JobId { get; set; }
		public string Message { get; set; }
        public string Status { get; set; }
        public decimal Price { get; set; }
        public string FreelancerId { get; set; }
        public string FreelancerName { get; set; }
    }
}
