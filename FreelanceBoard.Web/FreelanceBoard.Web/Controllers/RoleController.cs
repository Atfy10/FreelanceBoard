using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FreelanceBoard.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        [HttpPost("add")]
        public async Task<IActionResult> CreateRole()
        {
            return Ok();
        }
    }
}
