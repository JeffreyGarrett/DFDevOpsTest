
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net;
using System.Linq;

namespace MyTesting
{
    public static class DevOpsStarter
    {
        [FunctionName("DevOpsStarter")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)]
            HttpRequestMessage req, 
            [OrchestrationClient] DurableOrchestrationClient starter,
            ILogger log)
        {
            log.LogInformation("DevOpsTest HTTP Triggered");

            dynamic data = await req.Content.ReadAsAsync<object>();

            if (data?.delay == null || data?.speed == null)
                return req.CreateResponse(HttpStatusCode.BadRequest, "Please pass delay and speed in the request body (in seconds)");

            int delay = Convert.ToInt32(data?.delay);
            int speed = Convert.ToInt32(data?.speed);

            log.LogInformation($"Starting DevOpsTest with a {delay} delay and {speed} speed");

            var orchestrationId = await starter.StartNewAsync("O_DevOps", new {delay, speed});
            return starter.CreateCheckStatusResponse(req, orchestrationId);
        }
    }
}
