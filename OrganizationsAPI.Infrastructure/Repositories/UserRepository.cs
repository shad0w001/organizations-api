using Dapper;
using OrganizationsAPI.Domain.Entities;
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
    public class UserRepository : IUserRepository
    {
        private readonly DapperContext _context;

        public UserRepository(DapperContext context)
        {
            _context = context;
        }

        public async void Create(User user)
        {
            const string sql = @"INSERT INTO Users
                (Id, CreatedAt, IsDeleted, Username, PassHash, Salt, RoleId)
                VALUES (@Id, @CreatedAt, @IsDeleted, @Username, @PassHash, @Salt, @RoleId)";

            using (var client = _context.CreateSqlServerConnection())
            {
                client.Open();

                var rows = await client.ExecuteAsync(sql, user);

                client.Close();
            }
        }

        public async Task<User> GetUserByName(string username)
        {
            const string sql = @"SELECT * FROM Users WHERE IsDeleted = 0 AND Username = @Username";

            using (var client = _context.CreateSqlServerConnection())
            {
                client.Open();

                var user = await client.QueryFirstOrDefaultAsync<User>
                    (sql, new
                    {
                        Username = username
                    });

                client.Close();

                return user;
            }
        }

        public async Task<int> SoftDelete(string id)
        {
            const string sql = @"UPDATE Users
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
