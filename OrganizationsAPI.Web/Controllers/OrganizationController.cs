using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OrganizationsAPI.Web.Controllers
{
    [Route("api/organizations")]
    [ApiController]
    public class OrganizationController : ControllerBase
    {
        [HttpGet("get_all_organizations")]
        public IActionResult GetAllOrganizations()
        {
            return Ok();
        }

        [HttpGet("get_by_id/raw/{id}")]
        public IActionResult GetOrganizationById(string id)
        {
            return Ok();
        }

        [Authorize]
        [HttpGet("get_by_id/pdf/{id}")]
        public IActionResult GetPdfByOrganizationId(string id)
        {
            return Ok();
        }

        [Authorize]
        [HttpPost("create")]
        public IActionResult CreateOrganization()
        {
            return NoContent();
        }

        [Authorize]
        [HttpPut("update_by_id/{id}")]
        public IActionResult UpdateOrganizationById([FromRoute] string id)
        {
            return Ok(id);
        }

        [Authorize]
        [HttpDelete("delete/{id}")]
        public IActionResult DeleteOrganizationById(string id)
        {
            return Ok(id);
        }

    }
}
