using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrganizationsAPI.Appllication.DTOs.UserDTOs.LoginUserDTOs;
using OrganizationsAPI.Appllication.Interfaces.UserServices;

namespace OrganizationsAPI.Web.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }


        [HttpPost("register")]
        public IActionResult Register([FromBody] LoginUserRequestDTO userDTO)
        {
            var result = _service.RegisterUser(userDTO);

            if (result.IsFailure)
            {
                return StatusCode(500, result.Error);
            }

            return Ok(result.Value);
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginUserRequestDTO userDTO)
        {
            var result = _service.LoginUser(userDTO);

            if (result.IsFailure)
            {
                return StatusCode(500, result.Error);
            }

            return Ok(result.Value);
        }

        [Authorize]
        [HttpDelete("delete/{username}")]
        public IActionResult Delete([FromRoute] string username)
        {
            var result = _service.DeleteUser(username);

            if (result.IsFailure)
            {
                return StatusCode(500, result.Error);
            }

            return Ok(result.Value);
        }
    }
}
