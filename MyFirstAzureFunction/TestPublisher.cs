using MassTransit;
using Microsoft.Azure.WebJobs.Extensions.DurableTask.ContextImplementations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MyFirstAzureFunction;

public class TestPublisher(ILogger<TestPublisher> logger, IServiceProvider provider) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = provider.CreateScope();
        var client = scope.ServiceProvider.GetRequiredService<IRequestClient<Goodbye>>();

        var response = await client.GetResponse<Goodbye>(new Welcome(Guid.NewGuid().ToString()), stoppingToken);
        var msg = response.Message;

        logger.LogInformation("Received response for {InstanceId}", msg.InstanceId);
    }
}

public class WelcomeConsumer3(ILogger<WelcomeConsumer3> logger, IDurableClientFactory clientFactory)
    : IConsumer<Welcome>
{
    public async Task Consume(ConsumeContext<Welcome> context)
    {
        var message = context.Message;

        // var instanceId = await client.ScheduleNewOrchestrationInstanceAsync(
        //nameof(LowTouchAuditOrchestration.RunLowTouchAuditOrchestrator),
        //ltaInput);

        var durableClient = clientFactory.CreateClient();

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