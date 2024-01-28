using OrganizationsAPI.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationsAPI.Domain.Errors
{
    public static class OrganizationErrors
    {
        public static readonly Error InvalidIdInput = 
            new("Organization.InvalidIdInput", "The ID field cannot be empty");

        public static readonly Error InvalidInput =
            new("Organization.InvalidInput", "The query cannot be empty.");

        public static readonly Error NotFound =
            new("Organization.NotFound", "The organization with the provided ID does not exist.");

        public static readonly Error NoResourcesFound =
            new("Organization.NoResourcesFound", "There are no available resources for this request.");
    }
}
