using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationsAPI.Infrastructure.DbContext
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _sqlServerConnectionString;
        private readonly string _sqliteConnectionString;

        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _sqlServerConnectionString = _configuration.GetConnectionString("SqlServer");
            _sqliteConnectionString = _configuration.GetConnectionString("Sqlite");
        }

        public IDbConnection CreateSqlServerConnection()
            => new SqlConnection(_sqlServerConnectionString);
        public IDbConnection CreateSqLiteConnection()
            => new SqliteConnection(_sqliteConnectionString);
    }
}
