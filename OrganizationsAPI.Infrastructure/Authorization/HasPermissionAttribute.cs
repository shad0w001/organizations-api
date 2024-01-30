using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using OrganizationsAPI.Domain.Entities.Authentication;

namespace OrganizationsAPI.Infrastructure.Authorization
{
    public sealed class HasPermissionAttribute : AuthorizeAttribute
    {
        public HasPermissionAttribute(string permission)
            : base(policy: permission) { }
    }
}
