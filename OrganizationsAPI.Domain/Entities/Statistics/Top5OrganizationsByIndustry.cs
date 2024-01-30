using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationsAPI.Domain.Entities.Statistics
{
    public class Top5OrganizationsByIndustry
    {
        public string? Industry { get; set; }
        public string? LastUpdated { get; set; }
        public ICollection<Organization?>? Organizations { get; set; }
    }
}
