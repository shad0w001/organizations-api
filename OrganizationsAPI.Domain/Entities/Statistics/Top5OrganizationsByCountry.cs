﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationsAPI.Domain.Entities.Statistics
{
    public class Top5OrganizationsByCountry
    {
        public string? Country { get; set; }
        public string? LastUpdated { get; set; }
        public ICollection<Organization?>? Organizations { get; set; }
    }
}
