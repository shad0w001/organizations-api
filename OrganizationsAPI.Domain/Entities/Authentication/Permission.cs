using OrganizationsAPI.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationsAPI.Domain.Entities.Authentication
{
    public class Permission : Entity
    {
        public string PermissionName { get; set; }

        public static Permission ReadAccessPermission() => new() { PermissionName = "ReadAccess" };
        public static Permission WriteAccessPermission() => new() { PermissionName = "WriteAccess" };
        public static Permission FullAccessPermission() => new() { PermissionName = "FullAccess" };
    }
}
