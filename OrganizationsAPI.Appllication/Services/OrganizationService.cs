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
using System.Xml.Linq;

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

        public Result<Organization> GetOrganizationById(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return Result.Failure<Organization>(OrganizationErrors.InvalidIdInput);
            }

            var organization = _repository.GetById(id);

            if (organization.Result is null)
            {
                return Result.Failure<Organization>(OrganizationErrors.NotFound);
            }

            return Result.Success(organization.Result);
        }

        public Result<Organization> GetOrganizationByName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return Result.Failure<Organization>(OrganizationErrors.InvalidIdInput);
            }

            var organization = _repository.GetByName(name);

            if(organization.Result is null)
            {
                return Result.Failure<Organization>(OrganizationErrors.NotFound);
            }

            return Result.Success(organization.Result);
        }
        public Result<string> CreateOrganization(OrganizationRequestDTO organizationDTO)
        {
            if (organizationDTO is null)
            {
                return Result.Failure<string>(OrganizationErrors.InvalidInput);
            }

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

            return Result.Success("The organization has been created successfully");
        }

        public Result<string> UpdateOrganization(string name, OrganizationRequestDTO organizationDTO)
        {
            if (string.IsNullOrEmpty(name))
            {
                return Result.Failure<string>(OrganizationErrors.InvalidIdInput);
            }

            if (organizationDTO is null)
            {
                return Result.Failure<string>(OrganizationErrors.InvalidInput);
            }

            var organizationfromDb = _repository.GetByName(name).Result;

            if (organizationfromDb is null)
            {
                return Result.Failure<string>(OrganizationErrors.NotFound);
            }

            Organization organization = new Organization
            {
                Id = organizationfromDb.Id,
                Name = organizationDTO.Name,
                Website = organizationDTO.Website,
                Country = organizationDTO.Country,
                Description = organizationDTO.Description,
                Founded = organizationDTO.Founded,
                Industry = organizationDTO.Industry,
                NumberOfEmployees = organizationDTO.NumberOfEmployees,
            };

            var affectedRows = _repository.Update(organization);

            if(affectedRows.Result == 0)
            {
                return Result.Failure<string>(OrganizationErrors.NotFound);
            }

            return Result.Success("The organization has been updated successfully");
        }

        public Result<string> DeleteOrganization(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return Result.Failure<string>(OrganizationErrors.InvalidIdInput);
            }

            var organizationfromDb = _repository.GetByName(name).Result;

            if (organizationfromDb is null)
            {
                return Result.Failure<string>(OrganizationErrors.NotFound);
            }

            var affectedRows = _repository.SoftDelete(name);

            if (affectedRows.Result == 0)
            {
                return Result.Failure<string>(OrganizationErrors.NotFound);
            }

            return Result.Success("The organization has been deleted successfully");
        }
    }
}
