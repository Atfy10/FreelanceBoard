namespace FreelanceBoard.MVC.Models
{
	public class UserProfileViewModel
	{
		public string Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string UserName { get; set; }
		public string Email { get; set; }
		public string PhoneNumber { get; set; }
		public bool IsBanned { get; set; }
		public ProfileViewModel Profile { get; set; }
		public List<SkillViewModel> Skills { get; set; }
		public List<ProjectViewModel> Projects { get; set; }
	}
}
