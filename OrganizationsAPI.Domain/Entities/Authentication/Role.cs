﻿using OrganizationsAPI.Domain.Abstractions;
using OrganizationsAPI.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationsAPI.Domain.Entities.Authentication
{
    public class Role : Entity
    {
        public string RoleName { get; set; }

        public static Role Memeber() => new() { RoleName = "Member" };
        public static Role Admin() => new () { RoleName = "Admin" };
    }
}
