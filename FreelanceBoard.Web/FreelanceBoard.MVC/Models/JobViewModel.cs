namespace FreelanceBoard.MVC.Models
{
    public class JobViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime DateCreated { get; set; }
        public List<string> skillNames { get; set; } = new();
        public List<string> Categories { get; set; } = new();
    }
}
