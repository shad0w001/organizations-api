using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrganizationsAPI.Appllication.Interfaces;
using OrganizationsAPI.Infrastructure.Authorization;
using System.Diagnostics.Metrics;

namespace OrganizationsAPI.Web.Controllers
{
    [Route("api/stats")]
    [ApiController]
    public class StatisticController : ControllerBase
    {
        private readonly IStatisticsService _service;
        public StatisticController(IStatisticsService service)
        {
            _service = service;
        }

        [HasPermission(AuthPermissions.ReadAccess)]
        [HttpGet("get_organization_count_by_country/{country}")]
        public IActionResult GetOrganizationCountByCountry(string country)
        {
            var result = _service.GetOrganizationsCountForCountryByName(country);

            if (result.IsFailure)
            {
                return StatusCode(500, result.Error);
            }

            return Ok(result.Value);
        }

        [HasPermission(AuthPermissions.ReadAccess)]
        [HttpGet("get_organization_count_by_industry/{industry}")]
        public IActionResult GetOrganizationCountByIndustry(string industry)
        {
            var result = _service.GetOrganizationsCountForIndustryByName(industry);

            if (result.IsFailure)
            {
                return StatusCode(500, result.Error);
            }

            return Ok(result.Value);
        }

        [HasPermission(AuthPermissions.ReadAccess)]
        [HttpGet("get_top_5_organizations_by_country/{country}")]
        public IActionResult GetTop5OrganizationsByCountry([FromRoute] string country)
        {
            var result = _service.GetTop5OrganizationsForCountryByName(country);

            if (result.IsFailure)
            {
                return StatusCode(500, result.Error);
            }

            return Ok(result.Value);
        }

        [HasPermission(AuthPermissions.ReadAccess)]
        [HttpGet("get_top_5_industries_by_count/{name}")]
        public IActionResult GetTop5IndustriesByCount([FromRoute] string name)
        {
            var result = _service.GetTop5OrganizationsForIndustryByName(name);

            if (result.IsFailure)
            {
                return StatusCode(500, result.Error);
            }

            return Ok(result.Value);
        }
    }
}
