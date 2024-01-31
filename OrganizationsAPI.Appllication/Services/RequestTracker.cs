using OrganizationsAPI.Appllication.Interfaces;
using OrganizationsAPI.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationsAPI.Appllication.Services
{
    public class RequestTracker : IRequestTracker
    {
        private readonly List<RequestInfo> _requests;

        public RequestTracker()
        {
            _requests = new List<RequestInfo>();
        }

        public void AddRequest(RequestInfo requestInfo)
        {
            _requests.Add(requestInfo);
        }

        public IEnumerable<RequestInfo> GetRequests()
        {
            return _requests;
        }
    }
}
