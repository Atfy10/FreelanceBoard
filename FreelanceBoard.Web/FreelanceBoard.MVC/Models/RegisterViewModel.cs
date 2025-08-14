using System.ComponentModel.DataAnnotations;

namespace FreelanceBoard.MVC.Models
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        [MinLength(3, ErrorMessage = "First name must be at least 3 characters long.")]
		public string FirstName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Last name cannot be longer than 50 characters.")]
        [MinLength(3, ErrorMessage = "Last name must be at least 3 characters long.")]
		public string LastName { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]

        public string Role { get; set; }
    }

}
