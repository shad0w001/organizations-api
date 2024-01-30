﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationsAPI.Infrastructure.Authorization.PermissionService
{
    public interface IPermissionService
    {
        Task<HashSet<string>> GetPermissionsAsync(string id);
    }
}
