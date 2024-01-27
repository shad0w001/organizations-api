using OrganizationsAPI.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationsAPI.Domain.Entities
{
    public class Organization : Entity
    {
        public string Name { get; set; }
        public string Website { get; set; }
        public string Country { get; set; }
        public string Description { get; set; }
        public int Founded { get; set; }
        public string Industry { get; set; }
        public int NumberOfEmployees { get; set; }        
    }
}
