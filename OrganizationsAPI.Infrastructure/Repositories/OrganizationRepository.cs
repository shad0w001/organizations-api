using OrganizationsAPI.Domain.Entities;
using OrganizationsAPI.Domain.RepositoryInterfaces;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrganizationsAPI.Infrastructure.DbContext;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace OrganizationsAPI.Infrastructure.Repositories
{
    public class OrganizationRepository : IOrganizationRepository
    {
        private readonly DapperContext _context;

        public OrganizationRepository(DapperContext context)
        {
            _context = context;
        }
        public async Task<ICollection<Organization>> GetAll()
        {
            const string sql = @"SELECT * FROM Organizations WHERE IsDeleted = 0";

            using (var client = _context.CreateSqlServerConnection())
            {
                client.Open();

                var organizations = await client.QueryAsync<Organization>(sql);

                client.Close();

                return organizations.ToList();
            }
        }

        public async Task<Organization> GetById(string id)
        {
            const string sql = @"SELECT * FROM Organizations WHERE IsDeleted = 0 AND Id = @Id";

            using (var client = _context.CreateSqlServerConnection())
            {
                client.Open();

                var organization = await client.QueryFirstOrDefaultAsync<Organization>
                    (sql, new
                    {
                        Id = id
                    });

                client.Close();

                return organization;
            }
        }

        public async Task<Organization> GetByName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }

            const string sql = @"SELECT * FROM Organizations WHERE IsDeleted = 0 AND Name = @Name";

            using (var client = _context.CreateSqlServerConnection())
            {
                client.Open();

                var organization = await client.QueryFirstOrDefaultAsync<Organization>
                    (sql, new
                    {
                        Name = name
                    });

                client.Close();

                if (organization is null)
                {
                    return null;
                }

                return organization;
            }
        }

        public async void Insert(Organization organization)
        {
            const string sql = @"INSERT INTO Organizations
                (Id, CreatedAt, IsDeleted, Name, Website, Country, Description, Founded, Industry, NumberOfEmployees)
                VALUES (@Id, @CreatedAt, @IsDeleted, @Name, @Website, @Country, @Description, @Founded, @Industry, @NumberOfEmployees)";

            using (var client = _context.CreateSqlServerConnection())
            {
                client.Open();

                var rows = await client.ExecuteAsync(sql, organization);

                client.Close();
            }
        }

        public async Task<int> SoftDelete(string id)
        {
            const string sql = @"UPDATE Organizations
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

        public async Task<int> Update(Organization organizationDTO)
        {
            const string sql = @"UPDATE Organizations
                SET Name = @Name,
                Website = @Website,
                Country = @Country,
                Description = @Description,
                Founded = @Founded,
                Industry = @Industry,
                NumberOfEmployees = @NumberOfEmployees
            WHERE Id = @Id";

            using (var client = _context.CreateSqlServerConnection())
            {
                client.Open();

                int affectedRows = await client.ExecuteAsync(sql, organizationDTO);

                client.Close();

                return affectedRows;
            }
        }
    }
}
