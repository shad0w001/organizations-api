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
    }
}
