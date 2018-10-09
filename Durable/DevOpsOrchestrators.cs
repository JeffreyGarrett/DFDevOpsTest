using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MyTesting
{
    public static class DevOpsOrchestrators
    {
        [FunctionName("O_DevOps")]
        public static async Task<object> DevOpsOrchestrator(
            [OrchestrationTrigger] DurableOrchestrationContext context,
            ILogger log)
        {
            var input = context.GetInput<dynamic>();
            int delay = input?.delay;
            int speed = input?.speed;

            DateTime deadline = context.CurrentUtcDateTime.Add(TimeSpan.FromSeconds(delay));
            if (!context.IsReplaying)
                log.LogInformation("Delaying the activity until " + deadline);

            await context.CreateTimer(deadline, CancellationToken.None);

            if (!context.IsReplaying)
                log.LogInformation("About to run task activity");

            var runReturn = await context.CallActivityAsync<string>("A_RunTask", speed);

            return new
            {
                RunReturn = runReturn
            };
        }
    }
}
