using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using OrganizationsAPI.Appllication.Interfaces;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationsAPI.Infrastructure.Jobs
{
    public class WriteRequestDataToJsonJob : IJob
    {
        private readonly IRequestTracker _requestTracker;
        private readonly IOptionsSnapshot<RequestInfoOptions> _requestOptions;

        public WriteRequestDataToJsonJob(IRequestTracker requestTracker, IOptionsSnapshot<RequestInfoOptions> requestOptions)
        {
            _requestTracker = requestTracker;
            _requestOptions = requestOptions;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var requests = _requestTracker.GetRequests();
            var jsonRequests = JsonConvert.SerializeObject(requests);

            var directoryPath = _requestOptions.Value.JsonWritePath;

            if(!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            var fileName = $"{DateTime.UtcNow.ToString("yyyyMMdd")}.json";
            var filePath = Path.Combine(directoryPath, fileName);

            await File.WriteAllTextAsync(filePath, jsonRequests);
        }
    }
}
