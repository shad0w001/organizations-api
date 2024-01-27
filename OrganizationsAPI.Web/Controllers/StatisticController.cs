using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OrganizationsAPI.Web.Controllers
{
    [Route("api/statistics")]
    [ApiController]
    public class StatisticController : ControllerBase
    {
        [HttpGet("get_organization_count_by_country/{country}")]
        public IActionResult GetOrganizationCountByCountry(string country)
        {
            return Ok(country);
        }

        [HttpGet("get_organization_count_by_industry/{industry}")]
        public IActionResult GetOrganizationCountByIndustry(string industry)
        {
            return Ok(industry);
        }
    }
}
