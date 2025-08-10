using System.Security.Claims;

namespace FreelanceBoard.MVC.Extensions
{
	public static class ClaimsPrincipalExtensions
	{
		public static string? GetUserId(this ClaimsPrincipal user)
		{
			return user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
		}

		public static string? GetEmail(this ClaimsPrincipal user)
		{
			return user.FindFirst(ClaimTypes.Email)?.Value;
		}

		public static string? GetAccessToken(this ClaimsPrincipal user)
		{
			return user.FindFirst("access_token")?.Value;
		}

		public static string? GetFullName(this ClaimsPrincipal user)
		{
			return user.FindFirst(ClaimTypes.Name)?.Value;
		}

		public static string? GetRole(this ClaimsPrincipal user)
		{
			return user.FindFirst(ClaimTypes.Role)?.Value;
		}
	}
}
