using FreelanceBoard.MVC.Models;

namespace FreelanceBoard.MVC.Services.Interfaces
{
	public interface IUserService
	{
		Task<bool> RegisterAsync(RegisterViewModel model);
		Task LoginAsync(LoginViewModel model, HttpContext httpContext);
		Task LogoutAsync(HttpContext httpContext);
		Task<UserProfileViewModel> GetProfileAsync(HttpContext httpContext);
		Task ChangePasswordAsync(ChangePasswordViewModel model, HttpContext httpContext);

		Task AddProject(AddProjectViewModel model , HttpContext httpContext);

		Task AddSkillAsync(AddSkillViewModel model, HttpContext httpContext);

		Task RemoveSkillAsync(RemoveSkillViewModel model, HttpContext httpContext);

		Task UpdateProfileAsync(UserProfileViewModel model, HttpContext httpContext);

		Task DeleteProjectAsync(int projectId, HttpContext httpContext);





	}
}
