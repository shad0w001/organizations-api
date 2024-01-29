using OrganizationsAPI.Domain.Abstractions;
using OrganizationsAPI.Domain.Entities.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationsAPI.Infrastructure.DbManager.Scripts
{
    internal static class DbSeedingScripts
    {
        public static List<Permission> permissions = new List<Permission>()
        {
            Permission.ReadAccessPermission(),
            Permission.WriteAccessPermission(),
            Permission.FullAccessPermission()
        };

        public const string SEED_PERMISSIONS =
            @"IF NOT EXISTS(SELECT * FROM Permissions WHERE PermissionName = @PermissionName)
                BEGIN
                INSERT INTO Permissions (Id, CreatedAt, IsDeleted, PermissionName) 
                VALUES (@Id, @CreatedAt, @IsDeleted, @PermissionName)
                END";


        public static List<Role> roles = new()
        {
            Role.Memeber(),
            Role.Admin()
        };

        public const string SEED_ROLES =
            @"IF NOT EXISTS(SELECT * FROM Roles WHERE RoleName = @RoleName)
                BEGIN
                INSERT INTO Roles (Id, CreatedAt, IsDeleted, RoleName) 
                VALUES (@Id, @CreatedAt, @IsDeleted, @RoleName)
                END";


        public static List<RolePermission> rolePermissions = new List<RolePermission>()
        {
            
        };

        public const string SEED_ROLE_PERMISSIONS =
            @"IF NOT EXISTS(SELECT * FROM RolePermissions WHERE RoleId = @RoleId)
                BEGIN
                INSERT INTO RolePermissions (RoleId, PermissionId) 
                VALUES (@RoleId, @PermissionId) 
                END";
    }
}
