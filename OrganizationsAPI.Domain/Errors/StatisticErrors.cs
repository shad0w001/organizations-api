using OrganizationsAPI.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationsAPI.Domain.Errors
{
    public static class StatisticErrors
    {
        public static readonly Error InvalidInput =
            new("Statistics.InvalidIdInput", "The name field cannot be empty");

        public static readonly Error CountryOrganizationsNotFound =
            new("Statistics.CountryOrganizationsNotFound", "There are no organizations in the specified country");

        public static readonly Error IndustryOrganizationsNotFound =
            new("Statistics.IndustryOrganizationsNotFound", "There are no organizations for the specified industry");
    }
}
