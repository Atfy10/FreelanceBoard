using FreelanceBoard.Core.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace FreelanceBoard.Web.Extensions
{
	public static class ControllerExtensions
	{
		public static IActionResult HandleResult<T>(this ControllerBase controller, Result<T> result, int successStatusCode = 200)
		{
			if (result == null)
				return controller.StatusCode(500, new { Message = "Unexpected null result." });

			// If operation failed → directly use StatusCode from the result
			if (!result.IsSuccess)
			{
				// If StatusCode is not set, default to 500
				var code = result.StatusCode != 0 ? result.StatusCode : 500;
				return controller.StatusCode(code, result);
			}

			return successStatusCode switch
			{
				200 => controller.Ok(result),
				201 => controller.StatusCode(201, result),
				204 => controller.NoContent(),
				_ => controller.StatusCode(successStatusCode, result)
			};
		}

	}
}
