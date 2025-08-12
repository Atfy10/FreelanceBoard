namespace FreelanceBoard.MVC.Models
{
    public class JobProposalsViewModel
    {
        public decimal JobPrice { get; set; }
        public string JobTitle { get; set; }
        public DateTime JobDateCreated { get; set; }
        public List<ProposalViewModel> Proposals { get; set; }
    }
}
