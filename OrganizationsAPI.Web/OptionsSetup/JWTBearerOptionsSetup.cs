using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OrganizationsAPI.Infrastructure.Authentication;
using System.Text;

namespace OrganizationsAPI.Web.OptionsSetup
{
    public class JWTBearerOptionsSetup : IConfigureOptions<JwtBearerOptions>
    {
        private readonly JWTOptions _jwtOptions;

        public JWTBearerOptionsSetup(IOptions<JWTOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
        }
        public void Configure(JwtBearerOptions options)
        {
            options.TokenValidationParameters = new()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _jwtOptions.Issuer,
                ValidAudience = _jwtOptions.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_jwtOptions.SecretKey))
            };
        }
    }
}
