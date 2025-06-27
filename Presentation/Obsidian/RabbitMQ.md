1. In Shared, install the package : MassTransit.RabbitMQ
2. In Shared/Messaging
IEventPublisher.cs :
```
public interface IEventPublisher
{
    Task PublishAsync<T>(T @event, CancellationToken cancellationToken = default) where T : class;
}

```

EventPublisher.cs :
```
public class EventPublisher : IEventPublisher
{
    private readonly IPublishEndpoint _publishEndpoint;

    public EventPublisher(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    public async Task PublishAsync<T>(T @event, CancellationToken cancellationToken = default) where T : class
    {
        await _publishEndpoint.Publish(@event, cancellationToken);
    }
}
```

MassTransitExtensions.cs :
```
public static IServiceCollection AddMassTransitWithRabbitMq(
    this IServiceCollection services,
    IConfiguration configuration,
    Action<IBusRegistrationConfigurator>? configureConsumers = null)
{
    services.AddMassTransit(x =>
    {
        configureConsumers?.Invoke(x);

        x.UsingRabbitMq((context, cfg) =>
        {
            var connectionString = configuration.GetConnectionString("RabbitMQ")!;
            cfg.Host(connectionString);
            cfg.ConfigureEndpoints(context);
        });
    });

    services.AddScoped<IEventPublisher, EventPublisher>();

    return services;
}
```

Create an event which is a simple class. Then publish it with IEventPublisher.

To consume it in Catalog :
```
builder.Services.AddMassTransitWithRabbitMq(configuration, cfg =>
{
    cfg.AddConsumer<OrderPlacedConsumer>();
});
```

```
public class OrderPlacedConsumer : IConsumer<OrderPlacedEvent>
{
    public OrderPlacedConsumer()
    {

    }

    public Task Consume(ConsumeContext<OrderPlacedEvent> context)
    {
        var @event = context.Message;
        var orderId = @event.OrderId;

        return Task.CompletedTask;
    }
}

```

CLI to create RabbitMQ :

```
docker run -d `
 --name rabbitmq `
 -p 5672:5672 `
 -p 15672:15672 `
 -e RABBITMQ_DEFAULT_USER=guest `
 -e RABBITMQ_DEFAULT_PASS=guest `
 -v rabbitmq_data:/var/lib/rabbitmq `
 rabbitmq:3-management
```