using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreelanceBoard.Web.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TestController : ControllerBase
	{

		[HttpGet("test")]
		public IActionResult Test()
		{
			return Ok("Test successful");
		}
		[HttpGet("error")]
		public IActionResult Error()
		{
			return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred");
		}

		
	}
}
