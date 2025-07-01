using System.Reflection;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace MyFirstAzureFunction;

public static class ServiceCollectionMassTransit
{
    public static IServiceCollection ConfigureMassTransit(this IServiceCollection collection)
    {
        return collection.AddMassTransit(busRegistrationConfigurator =>
        {
            busRegistrationConfigurator.SetKebabCaseEndpointNameFormatter();

            var entryAssembly = Assembly.GetExecutingAssembly();

            busRegistrationConfigurator.AddConsumers(entryAssembly);
            busRegistrationConfigurator.AddSagaStateMachines(entryAssembly);
            busRegistrationConfigurator.AddSagas(entryAssembly);
            busRegistrationConfigurator.AddActivities(entryAssembly);
            busRegistrationConfigurator.UsingAzureServiceBus((context, config) =>
            {
                config.Host(
                    "Endpoint=sb://stat-stage-002.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=0m3OVQuIrJzT6jnXr1Y8uIheH/Q8wUfdW+ASbOOoZ4o=");
                config.ConfigureEndpoints(context);
            });
        });
    }
}