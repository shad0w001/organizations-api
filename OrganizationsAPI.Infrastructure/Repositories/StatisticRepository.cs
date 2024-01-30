using Dapper;
using OrganizationsAPI.Domain.Entities;
using OrganizationsAPI.Domain.Entities.Statistics;
using OrganizationsAPI.Domain.RepositoryInterfaces;
using OrganizationsAPI.Infrastructure.DbContext;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationsAPI.Infrastructure.Repositories
{
    public class StatisticRepository : IStatisticRepository
    {
        private readonly DapperContext _context;

        public StatisticRepository(DapperContext context)
        {
            _context = context;
        }
        public async Task<OrganizationCountByCountry> GetOrganizationCountForCountryByDate(string date)
        {
            const string sql = @"
                SELECT * FROM CountryCount
                WHERE LastUpdated = @Date
                LIMIT 1
            ";

            using (var client = _context.CreateSqLiteConnection())
            {
                client.Open();

                var entry = await client.QueryFirstOrDefaultAsync<OrganizationCountByCountry>(sql, new
                {
                    Date = date
                });

                client.Close();

                return entry;
            }
        }

        public async Task<int> GetOrganizationCountForIndustryByCountryyName(string name)
        {
            const string sql = @"
                SELECT COUNT(*) AS Count
                FROM Organizations
                WHERE Country = @Country AND IsDeleted = 0
                GROUP BY Country
            ";

            using (var client = _context.CreateSqlServerConnection())
            {
                client.Open();

                var count = await client.ExecuteScalarAsync<int>(sql, new
                {
                    Country = name
                });

                client.Close();

                return count;
            }
        }

        public async Task<OrganizationCountByIndustry> GetOrganizationCountForIndustryByDate(string date)
        {
            const string sql = @"
                SELECT * FROM IndustryCount
                WHERE LastUpdated = @Date
                LIMIT 1
            ";

            using (var client = _context.CreateSqLiteConnection())
            {
                client.Open();

                var entry = await client.QueryFirstOrDefaultAsync<OrganizationCountByIndustry>(sql, new
                {
                    Date = date
                });

                client.Close();

                return entry;
            }
        }

        public async Task<int> GetOrganizationCountForIndustryByIndustryName(string name)
        {
            const string sql = @"
                SELECT COUNT(*) AS Count
                FROM Organizations
                WHERE Industry = @Industry AND IsDeleted = 0
                GROUP BY Industry
            ";

            using (var client = _context.CreateSqlServerConnection())
            {
                client.Open();

                var count = await client.ExecuteScalarAsync<int>(sql, new
                {
                    Industry = name
                });

                client.Close();

                return count;
            }
        }

        public async Task<ICollection<Organization>> GetTop5OrganizationsForCountryByName(string name)
        {
            const string sql = @"
                SELECT TOP 5 *
                FROM Organizations
                WHERE Country = @Country AND IsDeleted = 0
                ORDER BY NumberOfEmployees DESC";

            using (var client = _context.CreateSqlServerConnection())
            {
                client.Open();

                var organizations = await client.QueryAsync<Organization>(sql, new
                {
                    Country = name
                });

                client.Close();

                return organizations.ToList();
            }
        }

        public async Task<ICollection<Organization>> GetTop5OrganizationsForIndustryByName(string name)
        {
            const string sql = @"
                SELECT TOP 5 *
                FROM Organizations
                WHERE Industry = @Industry AND IsDeleted = 0
                ORDER BY NumberOfEmployees DESC";

            using (var client = _context.CreateSqlServerConnection())
            {
                client.Open();

                var organizations = await client.QueryAsync<Organization>(sql, new
                {
                    Industry = name
                });

                client.Close();

                return organizations.ToList();
            }
        }

        public async void InsertOrganizationCountByCountry(OrganizationCountByCountry statistic)
        {
            const string sql = @"
                INSERT INTO CountryCount (Country, LastUpdated, Count)
                VALUES (@Country, @LastUpdated, @Count)
            ";

            using (var client = _context.CreateSqLiteConnection())
            {
                client.Open();

                await client.ExecuteAsync(sql, statistic);

                client.Close();
            }
        }

        public async void InsertOrganizationCountByIndustry(OrganizationCountByIndustry statistic)
        {
            const string sql = @"
                INSERT INTO IndustryCount (Industry, LastUpdated, Count)
                VALUES (@Industry, @LastUpdated, @Count)
            ";

            using (var client = _context.CreateSqLiteConnection())
            {
                client.Open();

                await client.ExecuteAsync(sql, statistic);

                client.Close();
            }
        }
    }
}
