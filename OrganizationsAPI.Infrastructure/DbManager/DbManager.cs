using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using OrganizationsAPI.Infrastructure.DbContext;
using OrganizationsAPI.Infrastructure.DbManager.Scripts;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationsAPI.Infrastructure.DbManager
{
    public class DbManager
    {
        public async static void EnsureDatabaseExistsAsync(string sqlServerConnectionString, string sqliteConnectionString)
        {
            using (var sqlServerConnection = new SqlConnection(sqlServerConnectionString))
            {
                sqlServerConnection.Open();

                await CreateSqlServerDatabaseSchema(sqlServerConnection);

                sqlServerConnection.Close();
            }

            using (var sqliteConnection = new SqliteConnection(sqliteConnectionString))
            {
                sqliteConnection.Open();

                await CreateSqliteDatabaseSchema(sqliteConnection);

                sqliteConnection.Close();
            }
        }

        private static async Task CreateSqlServerDatabaseSchema(SqlConnection connection)
        {
            // Create tables and define schema here
            using (connection)
            {
                await connection.ExecuteAsync(DbScripts.CREATE_SQL_SERVER_DATABASE_IF_NOT_EXISTS);
                await connection.ExecuteAsync(DbScripts.CREATE_ORGANIZATION_TABLE_IF_NOT_EXISTS);
                await connection.ExecuteAsync(DbScripts.CREATE_ROLE_TABLE_IF_NOT_EXISTS);
                await connection.ExecuteAsync(DbScripts.CREATE_USER_TABLE_IF_NOT_EXISTS);
            }
        }

        private static async Task CreateSqliteDatabaseSchema(SqliteConnection connection)
        {
            // Create tables and define schema here
            using (connection)
            {
                await connection.ExecuteAsync(DbScripts.CREATE_INSERTED_FILES_TABLE_IF_NOT_EXISTS);

                connection.Close();
            }
        }
    }
}
