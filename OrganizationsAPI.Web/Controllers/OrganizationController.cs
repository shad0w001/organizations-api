using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrganizationsAPI.Appllication.DTOs.OrganizationDTOs;
using OrganizationsAPI.Appllication.Interfaces;
using OrganizationsAPI.Domain.Entities;

namespace OrganizationsAPI.Web.Controllers
{
    [Route("api/organizations")]
    [ApiController]
    public class OrganizationController : ControllerBase
    {
        private readonly IOrganizationsService _service;

        public OrganizationController(IOrganizationsService service)
        {
            _service = service;   
        }

        [HttpGet("get_all_organizations")]
        public IActionResult GetAllOrganizations()
        {
            var organizations = _service.GetAllOrganizations();

            return Ok(organizations);
        }

        [HttpGet("get_by_id/raw/{id}")]
        public IActionResult GetOrganizationById(string id)
        {
            var organization = _service.GetOrganizationById(id);

            return Ok(organization);
        }

        [Authorize]
        [HttpGet("get_by_id/pdf/{id}")]
        public IActionResult GetPdfByOrganizationId(string id)
        {
            return Ok();
        }

        //[Authorize]
        [HttpPost("create")]
        public IActionResult CreateOrganization([FromBody] CreateOrganizationRequestDTO organizationDTO)
        {
            _service.CreateOrganization(organizationDTO);

            return NoContent();
        }

        //[Authorize]
        [HttpPut("update_by_id/{id}")]
        public IActionResult UpdateOrganizationById([FromRoute] string id, [FromBody] CreateOrganizationRequestDTO organizationDTO)
        {
            _service.UpdateOrganization(id, organizationDTO);

            return NoContent();
        }

        //[Authorize]
        [HttpDelete("delete/{id}")]
        public IActionResult DeleteOrganizationById(string id)
        {
            _service.DeleteOrganization(id);

            return NoContent();
        }

    }
}
