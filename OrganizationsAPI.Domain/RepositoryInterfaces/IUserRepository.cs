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
        public User GetUserByName(string username);
    }
}
