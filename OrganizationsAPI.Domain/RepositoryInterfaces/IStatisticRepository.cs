using OrganizationsAPI.Domain.Entities;
using OrganizationsAPI.Domain.Entities.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationsAPI.Domain.RepositoryInterfaces
{
    public interface IStatisticRepository
    {
        public Task<OrganizationCountByIndustry> GetOrganizationCountForIndustryByDate(string date);
        public Task<OrganizationCountByCountry> GetOrganizationCountForCountryByDate(string date);
        public void InsertOrganizationCountByIndustry(OrganizationCountByIndustry statistic);
        public void InsertOrganizationCountByCountry(OrganizationCountByCountry statistic);

        public Task<int> GetOrganizationCountForIndustryByIndustryName(string name);
        public Task<int> GetOrganizationCountForIndustryByCountryyName(string name);
        public Task<ICollection<Organization>> GetTop5OrganizationsForIndustryByName(string name);
        public Task<ICollection<Organization>> GetTop5OrganizationsForCountryByName(string name);
    }
}
