using System.ComponentModel.DataAnnotations;

namespace FreelanceBoard.MVC.Models
{
	public class JobCreateViewModel
	{


		[Required(ErrorMessage = "Job title is required")]
		[StringLength(100, ErrorMessage = "Title cannot be longer than 100 characters")]

		public string Title { get; set; }

		[Required(ErrorMessage = "Description is required")]
		[StringLength(1000, ErrorMessage = "Description cannot be longer than 1000 characters")]

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

		[Required(ErrorMessage = "Skills are required")]
		[MinLength(1, ErrorMessage = "At least one skill is required")]
		public List<string> SkillNames { get; set; } = new List<string>();

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
