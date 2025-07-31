using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceBoard.Core.Domain.Entities
{
    public class Review
    {
        [Key]
        public int Id { get; set; }
		[Range(1, 5)] //add rating range validation
		public int Rating { get; set; }
        public string Feedback { get; set; }
        public DateTime Date { get; set; }

        [ForeignKey("Contract")]

        public int ContractId { get; set; }
        public virtual Contract Contract { get; set; }

		//after review added Reviewer
		[ForeignKey("Reviewer")]
		public string ReviewerId { get; set; }
		public virtual ApplicationUser Reviewer { get; set; }
	}
}
