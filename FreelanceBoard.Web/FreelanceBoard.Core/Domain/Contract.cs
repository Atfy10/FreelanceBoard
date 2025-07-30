using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceBoard.Core.Domain
{
    public class Contract
    {
        [Key]
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        [ForeignKey("Job")]

        public int ContractId { get; set; }
        public virtual Job Job { get; set; }

        [ForeignKey("Payement")]

        public int PayementNumber { get; set; }
        public virtual Payement Payement { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }
    }
}
