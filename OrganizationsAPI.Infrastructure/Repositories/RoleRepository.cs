using Dapper;
using OrganizationsAPI.Domain.Entities.Authentication;
using OrganizationsAPI.Domain.RepositoryInterfaces;
using OrganizationsAPI.Infrastructure.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationsAPI.Infrastructure.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly DapperContext _context;

        public RoleRepository(DapperContext context)
        {
            _context = context;
        }
        public async void CreateRole(Role role)
        {
            const string sql = @"INSERT INTO Roles
                (Id, CreatedAt, IsDeleted, RoleName)
                VALUES (@Id, @CreatedAt, @IsDeleted, @RoleName)";

            using (var client = _context.CreateSqlServerConnection())
            {
                client.Open();

                var rows = await client.ExecuteAsync(sql, role);

                client.Close();
            }
        }

        public async Task<Role> GetRoleById(string id)
        {
            const string sql = @"SELECT * FROM Roles WHERE IsDeleted = 0 AND Id = @Id";

            using (var client = _context.CreateSqlServerConnection())
            {
                client.Open();

                var role = await client.QueryFirstOrDefaultAsync<Role>
                    (sql, new
                    {
                        Id = id
                    });

                client.Close();

                return role;
            }
        }

        public async Task<Role> GetRoleByName(string name)
        {
            const string sql = @"SELECT * FROM Roles WHERE IsDeleted = 0 AND RoleName = @Name";

            using (var client = _context.CreateSqlServerConnection())
            {
                client.Open();

                var role = await client.QueryFirstOrDefaultAsync<Role>
                    (sql, new
                    {
                        Name = name
                    });

                client.Close();

                return role;
            }
        }

        public async Task<int> UpdateRole(Role role)
        {
            const string sql = @"UPDATE Roles
                SET RoleName = @Name,
            WHERE Id = @Id";

            using (var client = _context.CreateSqlServerConnection())
            {
                client.Open();

                int affectedRows = await client.ExecuteAsync(sql, new
                {
                    Id = role.Id,
                    Name = role.RoleName                    
                });

                client.Close();

                return affectedRows;
            }
        }

        public async Task<int> SoftDelete(string id)
        {
            const string sql = @"UPDATE Roles
                SET IsDeleted = 1 WHERE Id = @Id";

            using (var client = _context.CreateSqlServerConnection())
            {
                client.Open();

                var affectedRows = await client.ExecuteAsync(sql, new
                {
                    Id = id
                });

                client.Close();

                return affectedRows;
            }
        }
    }
}
