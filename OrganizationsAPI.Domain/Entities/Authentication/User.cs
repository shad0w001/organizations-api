using OrganizationsAPI.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationsAPI.Domain.Entities.Authentication
{
    public class User : Entity
    {
        public string Username { get; set; }
        public string PassHash { get; set; }
        public string Salt { get; set; }
        public string RoleId { get; set; }
    }
}
