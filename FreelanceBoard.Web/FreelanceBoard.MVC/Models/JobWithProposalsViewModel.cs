namespace FreelanceBoard.MVC.Models
{
    public class JobWithProposalsViewModel
    {
        public int JobId { get; set; }
        public string JobTitle { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }
        public decimal Price { get; set; }
        public DateTime JobDateCreated { get; set; }

        // One job can have many proposals
        public List<ProposalViewModel> Proposals { get; set; } = new();
    }

}
