using OrganizationsAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationsAPI.Domain.RepositoryInterfaces
{
    public interface IOrganizationRepository
    {
        public Task<ICollection<Organization>> GetAll();
        public Task<Organization> GetById(string id);
        public Task<Organization> GetByName(string name);
        public void Insert(Organization organization);
        public Task<int> Update(Organization organization);
        public Task<int> SoftDelete(string id);
    }
}
