using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OrganizationsAPI.Appllication.Interfaces.UserServices;
using OrganizationsAPI.Domain.Entities.Authentication;
using OrganizationsAPI.Infrastructure.Authorization.PermissionService;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationsAPI.Infrastructure.Authentication
{
    public class JWTProvider : IJWTProvider
    {
        private readonly JWTOptions _options;
        private readonly IPermissionService _permissionService;

        public JWTProvider(IOptions<JWTOptions> options, IPermissionService permissionService)
        {
            _options = options.Value;
            _permissionService = permissionService;
        }

        public async Task<string> GenerateTokenAsync(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Name, user.Username)
            };

            var permissions = await _permissionService.GetPermissionsAsync(user.Id);

            foreach (var permission in permissions)
            {
                claims.Add(new("permission", permission));
            }

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
