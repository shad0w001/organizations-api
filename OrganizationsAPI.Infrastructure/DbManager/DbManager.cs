using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using OrganizationsAPI.Domain.Entities.Authentication;
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

            using (var sqlServerConnection = new SqlConnection(sqlServerConnectionString))
            {
                sqlServerConnection.Open();

                await SeedSqlServerDatabase(sqlServerConnection);

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
                await connection.ExecuteAsync(DbCreationScripts.CREATE_SQL_SERVER_DATABASE_IF_NOT_EXISTS);

                await connection.ExecuteAsync(DbCreationScripts.CREATE_ORGANIZATION_TABLE_IF_NOT_EXISTS);

                await connection.ExecuteAsync(DbCreationScripts.CREATE_ROLE_TABLE_IF_NOT_EXISTS);
                await connection.ExecuteAsync(DbCreationScripts.CREATE_USER_TABLE_IF_NOT_EXISTS);
                await connection.ExecuteAsync(DbCreationScripts.CREATE_PERMISSIONS_TABLE_IF_NOT_EXISTS);

                await connection.ExecuteAsync(DbCreationScripts.CREATE_USER_ROLES_TABLE_IF_NOT_EXISTS);
                await connection.ExecuteAsync(DbCreationScripts.CREATE_ROLE_PERMISSIONS_TABLE_IF_NOT_EXISTS);
            }
        }

        private static async Task SeedSqlServerDatabase(SqlConnection connection)
        {
            // Seed the database
            using (connection)
            {
                await connection.ExecuteAsync(DbSeedingScripts.SEED_PERMISSIONS, DbSeedingScripts.permissions);
                await connection.ExecuteAsync(DbSeedingScripts.SEED_ROLES, DbSeedingScripts.roles);
            }
        }

        private static async Task CreateSqliteDatabaseSchema(SqliteConnection connection)
        {
            // Create tables and define schema here
            using (connection)
            {
                await connection.ExecuteAsync(DbCreationScripts.CREATE_INSERTED_FILES_TABLE_IF_NOT_EXISTS);

                connection.Close();
            }
        }
    }
}
