using MassTransit;
using Microsoft.Azure.WebJobs.Extensions.DurableTask.ContextImplementations;
using Microsoft.Azure.WebJobs.Extensions.DurableTask.Options;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MyFirstAzureFunction;

public class TestPublisher(ILogger<TestPublisher> logger, IBus bus) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            logger.LogInformation("Publishing message at: {time}", DateTimeOffset.Now);
            await bus.Publish(new Welcome(DateTime.Now.ToString("HH:mm:ss zz")), stoppingToken);
            await Task.Delay(1000, stoppingToken);
        }
    }
}

public class WelcomeConsumer3(ILogger<WelcomeConsumer3> logger, IDurableClientFactory clientFactory) : IConsumer<Welcome>
{
    public Task Consume(ConsumeContext<Welcome> context)
    {
        var message = context.Message;

        // var instanceId = await client.ScheduleNewOrchestrationInstanceAsync(
        //nameof(LowTouchAuditOrchestration.RunLowTouchAuditOrchestrator),
        //ltaInput);

        var durableClient = clientFactory.CreateClient();
        
        logger.LogInformation("Consumer 3 received message: {message} at {time}", message, DateTimeOffset.Now);
        return Task.CompletedTask;
    }
}

public class GoodbyeConsumer(ILogger<GoodbyeConsumer> logger) : IConsumer<Goodbye>
{
    public Task Consume(ConsumeContext<Goodbye> context)
    {
        logger.LogInformation(context.Message.InstanceId);

        return Task.CompletedTask;
    }
}