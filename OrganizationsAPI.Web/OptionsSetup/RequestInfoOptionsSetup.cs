using Microsoft.Extensions.Options;
using OrganizationsAPI.Infrastructure.Authentication;
using OrganizationsAPI.Infrastructure.Jobs;

namespace OrganizationsAPI.Web.OptionsSetup
{
    public class RequestInfoOptionsSetup : IConfigureOptions<RequestInfoOptions>
    {
        private readonly IConfiguration _configuration;

        public RequestInfoOptionsSetup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void Configure(RequestInfoOptions options)
        {
            _configuration.GetSection("RequestOptions").Bind(options);
        }
    }
}
