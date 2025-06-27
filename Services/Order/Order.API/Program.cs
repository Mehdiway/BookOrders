using Grpc.Net.Client;
using Order.API.Configuration;
using Order.API.Exceptions;
using Order.Infrastructure.Configuration;
using Order.Infrastructure.EventHandlers;
using Scalar.AspNetCore;
using Shared.Messaging;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddOpenApi();

// Application services
builder.Services
    .AddEFCore(configuration)
    .AddMediatR()
    .AddFluentValidation()
    .AddPipelineBehaviors()
    .AddSwagger()
    .AddInfrastructureServices()
    .AddHttpClients();

builder.Services.AddMassTransitWithRabbitMQ(configuration, cfg =>
{
    cfg.AddConsumer<BookQuantitiesDecreasedEventConsumer>();
    cfg.AddConsumer<BookQuantitiesCannotBeDecreasedEventConsumer>();
});

var catalogServiceUrl = configuration.GetConnectionString("CatalogService");
builder.Services.AddSingleton(provider =>
{
    return GrpcChannel.ForAddress(catalogServiceUrl!);
});

var app = builder.Build();

app.MigrateDb();

app.UseMiddleware<GlobalExceptionHandler>();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    // Swagger
    app.UseSwagger();
    app.UseSwaggerUI();
    // Scalar
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
