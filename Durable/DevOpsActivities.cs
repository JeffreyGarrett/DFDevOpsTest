using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using System.Configuration;
using System.Threading.Tasks;


namespace MyTesting
{
    public static class ProcessVideoActivities
    {
        [FunctionName("A_RunTask")]
        public static async Task<string> Run(
            [ActivityTrigger] int speed,
            ILogger log)
        {
            log.LogInformation($"Running task. Will take {speed} seconds");
            // simulate doing the activity
            await Task.Delay(speed * 1000);

            return "ran task";
        }
    }
}
