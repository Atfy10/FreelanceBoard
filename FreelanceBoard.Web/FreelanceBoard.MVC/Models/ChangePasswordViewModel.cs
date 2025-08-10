using System.ComponentModel.DataAnnotations;

namespace FreelanceBoard.MVC.Models
{
    public class ChangePasswordViewModel
    {
        public string UserId { get; set; }

        [Required]
        public string CurrentPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Passwords do not match.")]
        public string ConfirmNewPassword { get; set; }
    }

}
