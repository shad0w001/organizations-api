using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationsAPI.Infrastructure.DbManager.Scripts
{
    internal static class DbScripts
    {
        public const string CREATE_SQL_SERVER_DATABASE_IF_NOT_EXISTS =
            @"IF NOT EXISTS (SELECT * FROM sys.databases WHERE NAME = 'OrganizationsAPI') 
            CREATE DATABASE OrganizationsAPI";

        public const string CREATE_ORGANIZATION_TABLE_IF_NOT_EXISTS =
            @"IF OBJECT_ID(N'Organizations', N'U') IS NULL
            CREATE TABLE Organizations(
                Id varchar(40) NOT NULL PRIMARY KEY,
                CreatedAt datetime NOT NULL,
                DeletedAt bit NULL,
                Name varchar(100) NOT NULL,
                Website varchar(265) NOT NULL,
                Country varchar(50) NOT NULL,
                Description text NOT NULL,
                Founded int NOT NULL,
                Industry varchar(100) NOT NULL,
                NumberOfEmployees int NOT NULL,
                UNIQUE(Id)
            );";

        public const string CREATE_USER_TABLE_IF_NOT_EXISTS =
            @"IF OBJECT_ID(N'Users', N'U') IS NULL
            CREATE TABLE Users(
                Id varchar(40) NOT NULL PRIMARY KEY,
                CreatedAt datetime NOT NULL,
                DeletedAt bit NOT NULL,
                Username varchar(64) NOT NULL,
                PassHash text NOT NULL,
                Salt text NOT NULL,
                UserRoleId varchar(40) NULL FOREIGN KEY REFERENCES [Roles](Id) ON DELETE SET NULL,
                UNIQUE(Username)
            );";

        public const string CREATE_ROLE_TABLE_IF_NOT_EXISTS =
            @"IF OBJECT_ID(N'Roles', N'U') IS NULL
            CREATE TABLE Roles(
                Id varchar(40) NOT NULL PRIMARY KEY,
                CreatedAt datetime NOT NULL,
                DeletedAt bit NOT NULL,
                RoleName varchar(20) NOT NULL,
                UNIQUE(RoleName)
            );";

        public const string CREATE_INSERTED_FILES_TABLE_IF_NOT_EXISTS =
            @"CREATE TABLE IF NOT EXISTS InsertedFiles (
                FileName TEXT NOT NULL,
                InsertionDate TEXT NOT NULL
            );";

    }
}
