using OrganizationsAPI.Appllication.DTOs.OrganizationDTOs;
using OrganizationsAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationsAPI.Appllication.Interfaces
{
    public interface IOrganizationsService
    {
        public ICollection<Organization> GetAllOrganizations();
        public Organization GetOrganizationById(string id);
        public void CreateOrganization(CreateOrganizationRequestDTO organizationDTO);
        public void UpdateOrganization(string id, CreateOrganizationRequestDTO organizationDTO);
        public void DeleteOrganization(string id);
    }
}
