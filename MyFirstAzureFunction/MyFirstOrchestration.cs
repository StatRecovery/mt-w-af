// MyFirstOrchestration.cs

using Microsoft.Azure.Functions.Worker; // For FunctionName
using Microsoft.DurableTask; // For TaskOrchestrationContext
using Microsoft.Extensions.Logging;

namespace MyFirstAzureFunction
{
    internal class MyFirstOrchestration(ILogger<MyFirstOrchestration> classLogger)
    {
        [Function(nameof(TheOrchestration))]
        public async Task<string> TheOrchestration(
            [OrchestrationTrigger] TaskOrchestrationContext context,
            Welcome welcome) // Directly bind the Welcome message as input
        {
            var logger = context.CreateReplaySafeLogger(nameof(TheOrchestration));
            // Updated logging to reflect Welcome.Name only
            logger.LogInformation($"Orchestration '{context.InstanceId}' started with Name: {welcome.Name}");

            // Removed calls to ProcessMessageActivity as MessageContent is no longer in Welcome

            logger.LogInformation($"Logging critical message for: {welcome.Name}");

            return await Task.FromResult(welcome.Name);
        }

        // Removed Activity Functions as they are no longer called by the orchestration
        // If you need activity functions, you would re-add them here and call them from TheOrchestration.
    }
}