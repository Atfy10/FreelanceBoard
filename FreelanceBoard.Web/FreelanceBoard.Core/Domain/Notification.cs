using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceBoard.Core.Domain
{
    public class Notification
    {
        [Key]
        public int Id { get; set; }
        public string Body { get; set; }
        public bool IsRead { get; set; }
        public string Status { get; set; }
        public DateTime Date { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
