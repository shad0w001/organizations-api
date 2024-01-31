using OrganizationsAPI.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationsAPI.Appllication.Interfaces
{
    public interface IRequestTracker
    {
        void AddRequest(RequestInfo requestInfo);
        IEnumerable<RequestInfo> GetRequests();
    }
}
