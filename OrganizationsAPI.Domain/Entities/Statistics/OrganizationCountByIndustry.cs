using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationsAPI.Domain.Entities.Statistics
{
    public class OrganizationCountByIndustry
    {
        public string? Industry { get; set; }
        public string? LastUpdated { get; set; }
        public int Count { get; set; }
    }
}
