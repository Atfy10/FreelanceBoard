namespace FreelanceBoard.MVC.Models
{
    public class ProposalViewModel
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }
        public decimal Price { get; set; }
        public string FreelancerName { get; set; }
    }
}
