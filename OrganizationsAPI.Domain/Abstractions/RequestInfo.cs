using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationsAPI.Domain.Abstractions
{
    public class RequestInfo
    {
        public string Path { get; set; }
        public string Method { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
