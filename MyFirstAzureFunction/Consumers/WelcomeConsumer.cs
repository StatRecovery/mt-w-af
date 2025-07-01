using MassTransit;
using Microsoft.Azure.WebJobs.Extensions.DurableTask.ContextImplementations;
using Microsoft.Azure.WebJobs.Extensions.DurableTask.Options;
using MyFirstAzureFunction.Models;
using MyFirstAzureFunction.Orchestration;

namespace MyFirstAzureFunction.Consumers;

public class WelcomeConsumer(IDurableClientFactory clientFactory) : IConsumer<Welcome>
{
    public async Task Consume(ConsumeContext<Welcome> context)
    {
        var message = context.Message;
        var durableClient = clientFactory.CreateClient(new DurableClientOptions { TaskHub = "MyDurableFunctionsHub" });
        var instanceId = await durableClient.StartNewAsync(
            nameof(MyFirstOrchestration.TheOrchestration),
            message
        );


        await context.RespondAsync(new Goodbye
        {
            InstanceId = instanceId
        });
    }
}