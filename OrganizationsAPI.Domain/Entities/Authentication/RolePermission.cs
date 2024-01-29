using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationsAPI.Domain.Entities.Authentication
{
    public class RolePermission
    {
        public string RoleId { get; set; }
        public string PermissionId { get; set; }
    }
}
