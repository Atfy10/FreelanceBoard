namespace FreelanceBoard.MVC.Models
{
    public class CreateJobViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public string UserId { get; set; }
        public List<string> SkillNames { get; set; }
    }

}
