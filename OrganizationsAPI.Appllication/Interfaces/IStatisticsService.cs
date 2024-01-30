using OrganizationsAPI.Domain.Abstractions;
using OrganizationsAPI.Domain.Entities;
using OrganizationsAPI.Domain.Entities.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationsAPI.Appllication.Interfaces
{
    public interface IStatisticsService
    {
        public Result<Top5OrganizationsByCountry> GetTop5OrganizationsForCountryByName(string countryName);
        public Result<Top5OrganizationsByIndustry> GetTop5OrganizationsForIndustryByName(string industryName);
        public Result<OrganizationCountByCountry> GetOrganizationsCountForCountryByName(string countryName);
        public Result<OrganizationCountByIndustry> GetOrganizationsCountForIndustryByName(string industryName);
    }
}
