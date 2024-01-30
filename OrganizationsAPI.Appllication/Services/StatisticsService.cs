using OrganizationsAPI.Appllication.Interfaces;
using OrganizationsAPI.Domain.Abstractions;
using OrganizationsAPI.Domain.Entities;
using OrganizationsAPI.Domain.Entities.Statistics;
using OrganizationsAPI.Domain.Errors;
using OrganizationsAPI.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationsAPI.Appllication.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly IStatisticRepository _repository;

        public StatisticsService(IStatisticRepository repository)
        {
            _repository = repository;
        }

        public Result<OrganizationCountByCountry> GetOrganizationsCountForCountryByName(string countryName)
        {
            if (string.IsNullOrEmpty(countryName))
            {
                return Result.Failure<OrganizationCountByCountry>(StatisticErrors.InvalidInput);
            }

            var dbentry = _repository.GetOrganizationCountForCountryByDate(DateTime.UtcNow.ToString("dd/MM/yyyy")).Result;
            if (dbentry is not null)
            {
                return Result.Success(dbentry);
            }

            var count = _repository.GetOrganizationCountForIndustryByCountryyName(countryName);

            if (count.Result == 0)
            {
                return Result.Failure<OrganizationCountByCountry>(StatisticErrors.CountryOrganizationsNotFound);
            }

            OrganizationCountByCountry statistic = new()
            {
                Country = countryName,
                LastUpdated = DateTime.UtcNow.ToString("dd/MM/yyyy"),
                Count = count.Result
            };

            _repository.InsertOrganizationCountByCountry(statistic);
            return Result.Success(statistic);
        }

        public Result<OrganizationCountByIndustry> GetOrganizationsCountForIndustryByName(string industryName)
        {
            if (string.IsNullOrEmpty(industryName))
            {
                return Result.Failure<OrganizationCountByIndustry>(StatisticErrors.InvalidInput);
            }

            var dbentry = _repository.GetOrganizationCountForIndustryByDate(DateTime.UtcNow.ToString("dd/MM/yyyy")).Result;
            if(dbentry is not null)
            {
                return Result.Success(dbentry);
            }

            var count = _repository.GetOrganizationCountForIndustryByCountryyName(industryName);

            if (count.Result == 0)
            {
                return Result.Failure<OrganizationCountByIndustry>(StatisticErrors.IndustryOrganizationsNotFound);
            }

            OrganizationCountByIndustry statistic = new()
            {
                Industry = industryName,
                LastUpdated = DateTime.UtcNow.ToString("dd/MM/yyyy"),
                Count = count.Result
            };

            _repository.InsertOrganizationCountByIndustry(statistic);
            return Result.Success(statistic);
        }

        public Result<Top5OrganizationsByCountry> GetTop5OrganizationsForCountryByName(string countryName)
        {
            if (string.IsNullOrEmpty(countryName))
            {
                return Result.Failure<Top5OrganizationsByCountry>(StatisticErrors.InvalidInput);
            }

            var organization = _repository.GetTop5OrganizationsForCountryByName(countryName);

            if (organization.Result.Count  == 0)
            {
                return Result.Failure<Top5OrganizationsByCountry>(StatisticErrors.CountryOrganizationsNotFound);
            }

            Top5OrganizationsByCountry statistic = new()
            {
                Country = countryName,
                LastUpdated = DateTime.UtcNow.ToString("dd/MM/yyyy"),
                Organizations = organization.Result
            };

            return Result.Success(statistic);
        }

        public Result<Top5OrganizationsByIndustry> GetTop5OrganizationsForIndustryByName(string industryName)
        {
            if (string.IsNullOrEmpty(industryName))
            {
                return Result.Failure<Top5OrganizationsByIndustry>(StatisticErrors.InvalidInput);
            }

            var organization = _repository.GetTop5OrganizationsForIndustryByName(industryName);

            if (organization.Result.Count == 0)
            {
                return Result.Failure<Top5OrganizationsByIndustry>(StatisticErrors.IndustryOrganizationsNotFound);
            }

            Top5OrganizationsByIndustry statistic = new()
            {
                Industry = industryName,
                LastUpdated = DateTime.UtcNow.ToString("dd/MM/yyyy"),
                Organizations = organization.Result
            };

            return Result.Success(statistic);
        }
    }
}
