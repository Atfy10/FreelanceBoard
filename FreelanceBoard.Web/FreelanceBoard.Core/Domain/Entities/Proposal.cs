using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceBoard.Core.Domain.Entities
{
    public class Proposal
    {
        [Key]
        public int Id { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }
        public decimal Price { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        
        [ForeignKey("Job")]
        public int JobId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual Job Job { get; set; }

    }
}
