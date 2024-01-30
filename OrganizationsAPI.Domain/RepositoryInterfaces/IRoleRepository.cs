using OrganizationsAPI.Domain.Entities.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationsAPI.Domain.RepositoryInterfaces
{
    public interface IRoleRepository
    {
        public void CreateRole(Role role);
        public Task<Role> GetRoleByName(string name);
        public void AddUserToRole(string userId, Role role);
        public Task<Role> GetRoleById(string id);
        public Task<int> UpdateRole(Role role);
        public Task<int> SoftDelete(string id);
    }
}
