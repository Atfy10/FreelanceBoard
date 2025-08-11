using FreelanceBoard.Core.Commands;
using FreelanceBoard.Core.Domain.Entities;
using FreelanceBoard.Core.Domain.Enums;
using FreelanceBoard.Core.Helpers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
public class ChangeProfilePictureCommandHandler : IRequestHandler<ChangeProfilePictureCommand, Result<string>>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly OperationExecutor _executor;

    public ChangeProfilePictureCommandHandler(
        UserManager<ApplicationUser> userManager,
        IHttpContextAccessor httpContextAccessor,
        OperationExecutor executor)
    {
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
        _executor = executor;
    }

    public async Task<Result<string>> Handle(ChangeProfilePictureCommand request, CancellationToken cancellationToken)
        => await _executor.Execute(async () =>
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
                return Result<string>.Failure(OperationType.Update.ToString(), "User not authenticated.");

            var user = await _userManager.Users
            .Include(x => x.Profile)
            .FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null)
                return Result<string>.Failure(OperationType.Update.ToString(), "User not found.");

            if (user.Profile == null)
                user.Profile = new Profile();


            if (request.ProfilePicture == null || request.ProfilePicture.Length == 0)
                return Result<string>.Failure(OperationType.Update.ToString(), "No file uploaded.");

            // Save file to wwwroot/images
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(request.ProfilePicture.FileName);
            var filePath = Path.Combine("wwwroot/uploads", fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await request.ProfilePicture.CopyToAsync(stream);
            }

            user.Profile.Image = "/uploads/" + fileName;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
                return Result<string>.Failure(OperationType.Update.ToString(), "Failed to update user.");

            return Result<string>.Success(user.Profile.Image, OperationType.Update.ToString());
        }, OperationType.Update);
}
