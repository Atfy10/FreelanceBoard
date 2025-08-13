using FreelanceBoard.Core.Domain.Entities;

namespace FreelanceBoard.MVC.Models
{
    public class ClientDashboardViewModel
    {
        public int Id { get; set; }                // Job ID
        public string Title { get; set; }          // Job title
        public string Description { get; set; }    // Job description
        public decimal Price { get; set; }         // Price/budget
        public DateTime DateCreated { get; set; }  // Posting date
        public DateTime Deadline {  get; set; }
        public List<Category> Categories { get; set; } = new();
        public List<Proposal> Proposals { get; set; } = new();
    }
}
