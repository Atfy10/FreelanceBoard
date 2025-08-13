using System.ComponentModel.DataAnnotations;

namespace FreelanceBoard.MVC.Models
{
	public class JobCreateViewModel
	{

		public JobCreateViewModel()
		{
			SkillNames = new List<string>(); // Initialize the list
			Deadline = DateTime.Now.AddDays(7); // Set default deadline
		}

		[Required(ErrorMessage = "Job title is required")]
		public string Title { get; set; }

		[Required(ErrorMessage = "Description is required")]
		public string Description { get; set; }

		[Required(ErrorMessage = "Category is required")]
		public string Category { get; set; }

		[Required(ErrorMessage = "Price is required")]
		[Range(0, double.MaxValue, ErrorMessage = "Price must be a positive number")]
		public decimal Price { get; set; }

		[Required(ErrorMessage = "Deadline is required")]
		[DataType(DataType.Date)]
		[FutureDate(ErrorMessage = "Deadline must be in the future")]
		public DateTime Deadline { get; set; }

		public List<string> SkillNames { get; set; } = new List<string>();

		// This will be set automatically, not in the form
		public string UserId { get; set; }

		public DateTime dateCreated { get; set; }

		public class FutureDateAttribute : ValidationAttribute
		{
			public override bool IsValid(object value)
			{
				if (value is DateTime date)
				{
					return date > DateTime.Now;
				}
				return false;
			}
		}
	}
}
