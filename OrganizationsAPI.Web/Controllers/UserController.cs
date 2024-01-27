using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OrganizationsAPI.Web.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost("register")]
        public IActionResult Register()
        {
            return Ok();
        }

        [HttpPost("login")]
        public IActionResult Login()
        {
            return Ok();
        }

        [Authorize]
        [HttpDelete("delete/{username}")]
        public IActionResult Delete(string username)
        {
            return NoContent();
        }
    }
}
