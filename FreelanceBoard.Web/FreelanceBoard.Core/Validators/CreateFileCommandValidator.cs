using FluentValidation;
using FreelanceBoard.Core.Commands;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;

namespace FreelanceBoard.Core.Validators
{
    public class CreateFileCommandValidator : AbstractValidator<CreateFileCommand>
    {
        private static readonly string[] AllowedExtensions =
        {
            ".pdf", ".doc", ".docx", ".xls", ".xlsx",
            ".jpg", ".jpeg", ".png", ".gif", ".txt"
        };

        private const long MaxFileSize = 10 * 1024 * 1024; // 10MB

        public CreateFileCommandValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required")
                .MaximumLength(100).WithMessage("Title cannot exceed 100 characters");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters");

            RuleFor(x => x.File)
                .NotNull().WithMessage("File is required")
                .Must(BeAValidFile).WithMessage("Invalid file type")
                .Must(BeWithinSizeLimit).WithMessage($"File size cannot exceed {MaxFileSize / (1024 * 1024)}MB");


        }

        private bool BeAValidFile(IFormFile file)
        {
            if (file == null) return false;

            var extension = System.IO.Path.GetExtension(file.FileName).ToLowerInvariant();
            return AllowedExtensions.Contains(extension);
        }

        private bool BeWithinSizeLimit(IFormFile file)
        {
            return file?.Length <= MaxFileSize;
        }

        private bool BeAValidGuid(string userId)
        {
            return Guid.TryParse(userId, out _);
        }
    }
}