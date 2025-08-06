using FreelanceBoard.Core.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace FreelanceBoard.Web.Extensions
{
	public static class ControllerExtensions
	{
        // better using of StatusCode in Result<T> to handle various outcomes
        public static IActionResult HandleResult<T>(this ControllerBase controller, Result<T> result, int successStatusCode = 200)
		{
			if (result == null)
				return controller.StatusCode(500, new { Message = "Unexpected null result." });

			if (!result.IsSuccess)
			{
				var message = result.Message?.ToLower() ?? "";

				if (message.Contains("not found") || message.Contains("key is not found"))
					return controller.NotFound(result);

				if (message.Contains("already exists") || message.Contains("duplicate") || message.Contains("conflict"))
					return controller.Conflict(result);

				if (message.Contains("unauthorized") || message.Contains("access denied"))
					return controller.Unauthorized(result);

				if (message.Contains("invalid") || message.Contains("bad request") || message.Contains("arg was null"))
					return controller.BadRequest(result);

				if (message.Contains("database error"))
					return controller.StatusCode(500, result);

				if (message.Contains("null reference"))
					return controller.StatusCode(500, result);

				return controller.StatusCode(500, result); 
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
