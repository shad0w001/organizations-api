using OrganizationsAPI.Appllication.DTOs.OrganizationDTOs;
using OrganizationsAPI.Appllication.Interfaces;
using OrganizationsAPI.Domain.Abstractions;
using OrganizationsAPI.Domain.Entities;
using OrganizationsAPI.Domain.Errors;
using OrganizationsAPI.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationsAPI.Appllication.Services
{
    public class OrganizationService : IOrganizationsService
    {
        private readonly IOrganizationRepository _repository;

        public OrganizationService(IOrganizationRepository repository)
        {
            _repository = repository;
        }

        public Result<ICollection<Organization>> GetAllOrganizations()
        {
            var organizations = _repository.GetAll();

            if(organizations.Result.Count == 0)
            {
                return Result.Failure<ICollection<Organization>>(OrganizationErrors.NoResourcesFound);
            }

            return Result.Success(organizations.Result);
        }

        public Organization GetOrganizationById(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }

            var organization = _repository.GetById(id);

            if(organization is null)
            {
                return null;
            }

            return organization.Result;
        }
        public void CreateOrganization(CreateOrganizationRequestDTO organizationDTO)
        {
            Organization organization = new Organization
            {
                Name = organizationDTO.Name,
                Website = organizationDTO.Website,
                Country = organizationDTO.Country,
                Description = organizationDTO.Description,
                Founded = organizationDTO.Founded,
                Industry = organizationDTO.Industry,
                NumberOfEmployees = organizationDTO.NumberOfEmployees,
            };

            _repository.Insert(organization);
        }

        public void UpdateOrganization(string id, CreateOrganizationRequestDTO organizationDTO)
        {
            if (string.IsNullOrEmpty(id))
            {
                return;
            }

            Organization organization = new Organization
            {
                Id = id,
                Name = organizationDTO.Name,
                Website = organizationDTO.Website,
                Country = organizationDTO.Country,
                Description = organizationDTO.Description,
                Founded = organizationDTO.Founded,
                Industry = organizationDTO.Industry,
                NumberOfEmployees = organizationDTO.NumberOfEmployees,
            };

            _repository.Update(organization);
        }

        public void DeleteOrganization(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return;
            }

            _repository.SoftDelete(id);
        }
    }
}
