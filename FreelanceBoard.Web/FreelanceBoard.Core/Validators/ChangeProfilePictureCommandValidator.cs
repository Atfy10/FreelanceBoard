using FluentValidation;
using FreelanceBoard.Core.Commands;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace FreelanceBoard.Core.Validators
{
    public class ChangeProfilePictureCommandValidator : AbstractValidator<ChangeProfilePictureCommand>
    {
        // Allowed image MIME types
        private static readonly string[] AllowedMimeTypes =
        {
            "image/jpeg",
            "image/png",
            "image/gif",
            "image/webp"
        };

        // Maximum file size (5MB)
        private const long MaxFileSize = 5 * 1024 * 1024;

        public ChangeProfilePictureCommandValidator()
        {
            RuleFor(x => x.ProfilePicture)
                .NotNull().WithMessage("Profile picture is required")
                .Must(BeAValidImage).WithMessage("Only JPEG, PNG, GIF or WEBP images are allowed")
                .Must(BeWithinSizeLimit).WithMessage($"File size cannot exceed {MaxFileSize / (1024 * 1024)}MB");
        }

        private bool BeAValidImage(IFormFile file)
        {
            if (file == null) return false;
            return AllowedMimeTypes.Contains(file.ContentType.ToLower());
        }

        private bool BeWithinSizeLimit(IFormFile file)
        {
            return file?.Length <= MaxFileSize;
        }
    }
}