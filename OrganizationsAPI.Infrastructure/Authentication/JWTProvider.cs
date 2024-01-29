using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OrganizationsAPI.Appllication.Interfaces.UserServices;
using OrganizationsAPI.Domain.Entities.Authentication;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationsAPI.Infrastructure.Authentication
{
    public class JWTProvider : IJWTProvider
    {
        private readonly JWTOptions _options;

        public JWTProvider(IOptions<JWTOptions> options)
        {
            _options = options.Value;
        }

        public string GenerateToken(User user)
        {
            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Name, user.Username)
            };

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_options.SecretKey)),
                    SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _options.Issuer,
                _options.Audience,
                claims,
                null,
                DateTime.UtcNow.AddHours(1),
                signingCredentials);

            string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenValue;
        }
    }
}
