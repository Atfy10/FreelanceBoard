namespace FreelanceBoard.MVC.Models
{
    public class CreateProposalViewModel
    {
        public int JobId { get; set; }
        public string FreelancerId { get; set; }
        public string Message { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; }
    }
}
