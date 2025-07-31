using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreelanceBoard.Core.Domain.Enums;

namespace FreelanceBoard.Core.Domain.Entities
{
    public class Proposal
    {
        [Key]
        public int Id { get; set; }
        public string Message { get; set; }
        public ProposalStatus Status { get; set; } //after review should be enum
        public decimal Price { get; set; }

        //better chang user to freelancer
  //      [ForeignKey("User")] 
  //      public string UserId { get; set; }
		//public virtual ApplicationUser User { get; set; }

		[ForeignKey("Freelancer")]
		public string FreelancerId { get; set; }
		public virtual ApplicationUser Freelancer { get; set; }

		[ForeignKey("Job")]
        public int JobId { get; set; }
        public virtual Job Job { get; set; }

    }
}
