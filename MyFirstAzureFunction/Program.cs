using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Hosting;
using MyFirstAzureFunction.Extension;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();

builder.Services
    .ConfigureMassTransit()
    .AddDurableClientFactory()
    //.AddApplicationInsightsTelemetryWorkerService()
    //.ConfigureFunctionsApplicationInsights()
    ;

builder.Build().Run();
