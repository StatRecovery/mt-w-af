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

        Wait(logger);


        foreach (var i in Enumerable.Range(1, 10))
        {
            logger.LogCritical("Instance #{I}", i);
            
            logger.LogInformation(
                "Orchestration '{ContextInstanceId}' started with Name: {WelcomeName}",
                context.InstanceId, 
                welcome.Name
            );
            
            logger.LogInformation("Logging critical message for: {WelcomeName}", welcome.Name);
        }

        return await Task.FromResult(welcome.Name);
    }


    private static void Wait(ILogger logger)
    {
        foreach (var i in Enumerable.Range(1, 30))
        {
            logger.LogCritical(30 - i + " seconds left...");
            Thread.Sleep(1000);
        }
    }
}