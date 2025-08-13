using FreelanceBoard.Core.Dtos;

namespace FreelanceBoard.MVC.Models
{
    public class ReviewViewModel
    {
        public int Rating { get; set; }
        public string Feedback { get; set; }
        public DateTime Date { get; set; }

        public ApplicationUserDto Reviewer { get; set; }
        public ContractDto Contract { get; set; }
    }
}
