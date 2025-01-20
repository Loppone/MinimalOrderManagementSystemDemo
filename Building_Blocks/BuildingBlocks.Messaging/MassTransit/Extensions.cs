namespace BuildingBlocks.Messaging.MassTransit;

/// <summary>
/// La configuraione per un consumer è attualmente troppo complessa e va rivista
/// la struttura. Un semplice extension method non è sufficiente.
/// </summary>
[Obsolete]
public static class Extentions
{
    public static IServiceCollection AddMessageBroker(
        this IServiceCollection services, 
        IConfiguration configuration, Assembly? assembly = null)
    {
        services.AddMassTransit(config =>
        {
            config.SetKebabCaseEndpointNameFormatter();

            if (assembly != null)
                config.AddConsumers(assembly);

            config.UsingRabbitMq((context, configurator) =>
            {
                configurator.Host(new Uri(configuration["MessageBroker:Host"]!), host =>
                {
                    host.Username(configuration["MessageBroker:UserName"]!);
                    host.Password(configuration["MessageBroker:Password"]!);
                });
                configurator.ConfigureEndpoints(context);
            });
        });

        return services;
    }
}