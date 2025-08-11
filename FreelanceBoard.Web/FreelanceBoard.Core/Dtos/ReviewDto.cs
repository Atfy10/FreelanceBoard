using FreelanceBoard.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceBoard.Core.Dtos
{
    public class ReviewDto
    {
        public int Rating { get; set; }
        public string Feedback { get; set; }
        public DateTime Date { get; set; }

        public ApplicationUserDto Reviewer { get; set; }
        public ContractDto Contract { get; set; }
    }
}
