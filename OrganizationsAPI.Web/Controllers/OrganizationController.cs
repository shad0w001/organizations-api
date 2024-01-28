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
            var result = _service.GetAllOrganizations();

            if (result.IsFailure)
            {
                return StatusCode(500, result.Error);
            }

            return Ok(result.Value);
        }

        [HttpGet("get_by_id/raw/{id}")]
        public IActionResult GetOrganizationById([FromRoute] string id)
        {
            var result = _service.GetOrganizationById(id);

            if (result.IsFailure)
            {
                return StatusCode(500, result.Error);
            }

            return Ok(result.Value);
        }

        [Authorize(Roles = "User")]
        [HttpGet("get_by_id/pdf/{id}")]
        public IActionResult GetPdfByOrganizationId([FromRoute] string id)
        {
            var result = _service.GetOrganizationById(id);

            if (result.IsFailure)
            {
                return StatusCode(500, result.Error);
            }

            //it will return a pdf file in the future
            return Ok(result.Value);
        }

        //[Authorize]
        [HttpPost("create")]
        public IActionResult CreateOrganization([FromBody] OrganizationRequestDTO organizationDTO)
        {
            var result = _service.CreateOrganization(organizationDTO);

            if (result.IsFailure)
            {
                return StatusCode(500, result.Error);
            }

            return Ok(result.Value);
        }

        //[Authorize]
        [HttpPut("update_by_id/{id}")]
        public IActionResult UpdateOrganizationById([FromRoute] string id, [FromBody] OrganizationRequestDTO organizationDTO)
        {
            var result = _service.UpdateOrganization(id, organizationDTO);

            if (result.IsFailure)
            {
                return StatusCode(500, result.Error);
            }

            return Ok(result.Value);
        }

        //[Authorize]
        [HttpDelete("delete/{id}")]
        public IActionResult DeleteOrganizationById([FromRoute] string id)
        {
            var result = _service.DeleteOrganization(id);

            if (result.IsFailure)
            {
                return StatusCode(500, result.Error);
            }

            return Ok(result.Value);
        }

    }
}
