using Dapper;
using Microsoft.AspNetCore.SignalR;
using OrganizationsAPI.Domain.Entities.Authentication;
using OrganizationsAPI.Infrastructure.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationsAPI.Infrastructure.Authorization.PermissionService
{
    public class PermissionService : IPermissionService
    {
        private readonly DapperContext _context;

        public PermissionService(DapperContext context)
        {
            _context = context;
        }
        public async Task<HashSet<string>> GetPermissionsAsync(string id)
        {
            string sql = @"
                    SELECT DISTINCT p.PermissionName
                    FROM Users u
                    INNER JOIN UserRoles ur ON u.Id = ur.UserId
                    INNER JOIN Roles r ON ur.RoleId = r.Id
                    INNER JOIN RolePermissions rp ON r.Id = rp.RoleId
                    INNER JOIN Permissions p ON rp.PermissionId = p.Id
                    WHERE u.Id = @Id";

            using (var client = _context.CreateSqlServerConnection())
            {
                client.Open();

                var permissions = await client.QueryAsync<string>(sql, new
                {
                    Id = id
                });

                var permissionHashSet = new HashSet<string>(permissions);

                client.Close();

                return permissionHashSet;

            }
        }
    }
}
