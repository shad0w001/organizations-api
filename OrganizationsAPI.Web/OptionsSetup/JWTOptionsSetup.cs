using Microsoft.Extensions.Options;
using OrganizationsAPI.Infrastructure.Authentication;

namespace OrganizationsAPI.Web.OptionsSetup
{
    public class JWTOptionsSetup : IConfigureOptions<JWTOptions>
    {
        private readonly IConfiguration _configuration;
        public JWTOptionsSetup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void Configure(JWTOptions options)
        {
            _configuration.GetSection("JwtAuthentication").Bind(options);
        }
    }
}
