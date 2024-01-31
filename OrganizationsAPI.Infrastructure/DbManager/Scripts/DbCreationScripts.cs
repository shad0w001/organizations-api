using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationsAPI.Infrastructure.DbManager.Scripts
{
    internal static class DbCreationScripts
    {
        public const string CREATE_SQL_SERVER_DATABASE_IF_NOT_EXISTS =
            @"IF NOT EXISTS (SELECT * FROM sys.databases WHERE NAME = 'OrganizationsAPI') 
            CREATE DATABASE OrganizationsAPI";

        public const string CREATE_ORGANIZATION_TABLE_IF_NOT_EXISTS =
            @"IF OBJECT_ID(N'Organizations', N'U') IS NULL
            CREATE TABLE Organizations(
                Id varchar(40) NOT NULL PRIMARY KEY,
                CreatedAt datetime NOT NULL,
                IsDeleted bit NOT NULL,
                Name varchar(100) NOT NULL,
                Website varchar(265) NOT NULL,
                Country varchar(50) NOT NULL,
                Description text NOT NULL,
                Founded int NOT NULL,
                Industry varchar(100) NOT NULL,
                NumberOfEmployees int NOT NULL,
                UNIQUE(Id, Name)
            );";

        public const string CREATE_USER_TABLE_IF_NOT_EXISTS =
            @"IF OBJECT_ID(N'Users', N'U') IS NULL
            CREATE TABLE Users(
                Id varchar(40) NOT NULL PRIMARY KEY,
                CreatedAt datetime NOT NULL,
                IsDeleted bit NOT NULL,
                Username varchar(64) NOT NULL,
                PassHash text NOT NULL,
                Salt text NOT NULL,
                UNIQUE(Id, Username)
            );";

        public const string CREATE_ROLE_TABLE_IF_NOT_EXISTS =
            @"IF OBJECT_ID(N'Roles', N'U') IS NULL
            CREATE TABLE Roles(
                Id varchar(40) NOT NULL PRIMARY KEY,
                CreatedAt datetime NOT NULL,
                IsDeleted bit NOT NULL,
                RoleName varchar(20) NOT NULL,
                UNIQUE(Id, RoleName)
            );";

        public const string CREATE_PERMISSIONS_TABLE_IF_NOT_EXISTS =
            @"IF OBJECT_ID(N'Permissions', N'U') IS NULL
            CREATE TABLE Permissions(
                Id varchar(40) NOT NULL PRIMARY KEY,
                CreatedAt datetime NOT NULL,
                IsDeleted bit NOT NULL,
                PermissionName varchar(20) NOT NULL,
                UNIQUE(Id, PermissionName)
            );";

        public const string CREATE_USER_ROLES_TABLE_IF_NOT_EXISTS =
            @"IF OBJECT_ID(N'UserRoles', N'U') IS NULL
            CREATE TABLE UserRoles(
                UserId varchar(40) FOREIGN KEY REFERENCES [Users](Id) ON DELETE SET NULL,
                RoleId varchar(40) FOREIGN KEY REFERENCES [Roles](Id) ON DELETE SET NULL
            );";

        public const string CREATE_ROLE_PERMISSIONS_TABLE_IF_NOT_EXISTS =
            @"IF OBJECT_ID(N'RolePermissions', N'U') IS NULL
            CREATE TABLE RolePermissions(
                RoleId varchar(40) FOREIGN KEY REFERENCES [Roles](Id) ON DELETE SET NULL,
                PermissionId varchar(40) FOREIGN KEY REFERENCES [Permissions](Id) ON DELETE SET NULL
            );";

        public const string CREATE_INSERTED_FILES_TABLE_IF_NOT_EXISTS =
            @"CREATE TABLE IF NOT EXISTS InsertedFiles (
                FileName TEXT NOT NULL,
                InsertionDate TEXT NOT NULL
            );";

        public const string CREATE_ORGANIZATION_COUNT_BY_COUNTRY_TABLE_IF_NOT_EXISTS =
            @"CREATE TABLE IF NOT EXISTS CountryCount (
                Country TEXT NOT NULL,
                LastUpdated TEXT NOT NULL,
                Count INT NOT NULL
            );";

        public const string CREATE_ORGANIZATION_COUNT_BY_INDUSTRY_TABLE_IF_NOT_EXISTS =
            @"CREATE TABLE IF NOT EXISTS IndustryCount (
                Industry TEXT NOT NULL,
                LastUpdated TEXT NOT NULL,
                Count INT NOT NULL
            );";

    }
}
