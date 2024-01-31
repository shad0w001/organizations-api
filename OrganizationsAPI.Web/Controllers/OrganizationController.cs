using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrganizationsAPI.Appllication.DTOs.OrganizationDTOs;
using OrganizationsAPI.Appllication.Interfaces;
using OrganizationsAPI.Domain.Entities;
using OrganizationsAPI.Domain.Entities.Authentication;
using OrganizationsAPI.Infrastructure.Authorization;
using OrganizationsAPI.Infrastructure.PdfGenerator;

namespace OrganizationsAPI.Web.Controllers
{
    [Route("api/organizations")]
    [ApiController]
    public class OrganizationController : ControllerBase
    {
        private readonly IOrganizationsService _service;
        private readonly IPdfGenerator _pdfGenerator;

        public OrganizationController(IOrganizationsService service, IPdfGenerator pdfGenerator)
        {
            _service = service;
            _pdfGenerator = pdfGenerator;
        }

        [HasPermission(AuthPermissions.ReadAccess)]
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

        [HasPermission(AuthPermissions.ReadAccess)]
        [HttpGet("get_by_id/raw/{name}")]
        public IActionResult GetOrganizationById([FromRoute] string name)
        {
            var result = _service.GetOrganizationByName(name);

            if (result.IsFailure)
            {
                return StatusCode(500, result.Error);
            }

            return Ok(result.Value);
        }

        //[HasPermission(AuthPermissions.ReadAccess)]
        [HttpGet("get_by_id/pdf/{name}")]
        public IActionResult GetPdfByOrganizationId([FromRoute] string name)
        {
            var result = _service.GetOrganizationByName(name);

            if (result.IsFailure)
            {
                return StatusCode(500, result.Error);
            }
            
            //it will return a pdf file in the future
            return File(_pdfGenerator.GenerateOrganizationPdf(result.Value), "application/pdf", "generated.pdf");
        }

        [HasPermission(AuthPermissions.WriteAccess)]
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

        [HasPermission(AuthPermissions.WriteAccess)]
        [HttpPut("update_by_name/{name}")]
        public IActionResult UpdateOrganizationById([FromRoute] string name, [FromBody] OrganizationRequestDTO organizationDTO)
        {
            var result = _service.UpdateOrganization(name, organizationDTO);

            if (result.IsFailure)
            {
                return StatusCode(500, result.Error);
            }

            return Ok(result.Value);
        }

        [HasPermission(AuthPermissions.FullAccess)]
        [HttpDelete("delete_by_name/{name}")]
        public IActionResult DeleteOrganizationById([FromRoute] string name)
        {
            var result = _service.DeleteOrganization(name);

            if (result.IsFailure)
            {
                return StatusCode(500, result.Error);
            }

            return Ok(result.Value);
        }

    }
}
