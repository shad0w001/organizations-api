using OrganizationsAPI.Domain.Entities;
using OrganizationsAPI.Domain.Entities.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationsAPI.Domain.RepositoryInterfaces
{
    public interface IUserRepository
    {
        public Task<User> GetUserByName(string username);
        public void Create(User user);
        public Task<int> SoftDelete(string id);
    }
}
