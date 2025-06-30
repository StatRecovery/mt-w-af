using Microsoft.Azure.Functions.Worker;
using Microsoft.DurableTask;
using Microsoft.Extensions.Logging;

namespace MyFirstAzureFunction;

internal class MyFirstOrchestration
{
    [Function(nameof(TheOrchestration))]
    public async Task<string> TheOrchestration(
        [OrchestrationTrigger] TaskOrchestrationContext context,
        Welcome welcome)
    {
        var logger = context.CreateReplaySafeLogger(nameof(TheOrchestration));
        logger.Log(LogLevel.Critical, welcome.Name);
        return await Task.FromResult(welcome.Name);
    }
}