using Microsoft.AspNetCore.Http;
using OrganizationsAPI.Appllication.Interfaces;
using OrganizationsAPI.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationsAPI.Appllication.MiddleWare
{
    public class RequestTrackingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IRequestTracker _requestTracker;

        public RequestTrackingMiddleware(RequestDelegate next, IRequestTracker requestTracker)
        {
            _next = next;
            _requestTracker = requestTracker;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var requestInfo = new RequestInfo
            {
                Path = context.Request.Path,
                Method = context.Request.Method,
                Timestamp = DateTime.UtcNow
            };

            _requestTracker.AddRequest(requestInfo);

            await _next(context);
        }
    }
}

