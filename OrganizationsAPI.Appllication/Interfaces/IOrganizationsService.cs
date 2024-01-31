using OrganizationsAPI.Appllication.DTOs.OrganizationDTOs;
using OrganizationsAPI.Domain.Abstractions;
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
        public Result<ICollection<Organization>> GetAllOrganizations();
        public Result<Organization> GetOrganizationById(string id);
        public Result<Organization> GetOrganizationByName(string name);
        public Result<string> CreateOrganization(OrganizationRequestDTO organizationDTO);
        public Result<string> UpdateOrganization(string name, OrganizationRequestDTO organizationDTO);
        public Result<string> DeleteOrganization(string name);
    }
}
