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
            @"
            -- Adding all Permissions to all Roles
            DECLARE @MemberRoleId UNIQUEIDENTIFIER = (SELECT Roles.Id FROM Roles WHERE RoleName = 'Member')
            DECLARE @AdminRoleId UNIQUEIDENTIFIER = (SELECT Roles.Id FROM Roles WHERE RoleName = 'Admin')

            INSERT INTO RolePermissions (RoleId, PermissionId)
            SELECT r.RoleId, p.Id
            FROM (
                SELECT @MemberRoleId AS RoleId
                UNION ALL
                SELECT @AdminRoleId AS RoleId
            ) r
            CROSS JOIN (
                SELECT p1.Id FROM Permissions p1 WHERE p1.PermissionName = 'ReadAccess'
                UNION ALL
                SELECT p2.Id FROM Permissions p2 WHERE p2.PermissionName = 'WriteAccess'
                UNION ALL
                SELECT p3.Id FROM Permissions p3 WHERE p3.PermissionName = 'FullAccess'
            ) p
            WHERE NOT EXISTS (
                SELECT 1 
                FROM RolePermissions rp 
                WHERE rp.RoleId = r.RoleId 
                AND rp.PermissionId = p.Id
            );

            -- Revoking FullAccess from Member role
            DECLARE @MemberRoleId UNIQUEIDENTIFIER = (SELECT Roles.Id FROM Roles WHERE RoleName = 'Member')
            DECLARE @AdminRoleId UNIQUEIDENTIFIER = (SELECT Roles.Id FROM Roles WHERE RoleName = 'Admin')

            DELETE FROM RolePermissions
            WHERE RoleId = @MemberRoleId 
            AND PermissionId IN (SELECT Id FROM Permissions WHERE PermissionName = 'FullAccess');

            -- Adding FullAccess to Admin role if not already granted
            DECLARE @MemberRoleId UNIQUEIDENTIFIER = (SELECT Roles.Id FROM Roles WHERE RoleName = 'Member')
            DECLARE @AdminRoleId UNIQUEIDENTIFIER = (SELECT Roles.Id FROM Roles WHERE RoleName = 'Admin')

            INSERT INTO RolePermissions (RoleId, PermissionId)
            SELECT @AdminRoleId, p.Id
            FROM Permissions p
            WHERE p.PermissionName = 'FullAccess'
            AND NOT EXISTS (
                SELECT 1 
                FROM RolePermissions rp 
                WHERE rp.RoleId = @AdminRoleId 
                AND rp.PermissionId = p.Id
            );";
    }
}
