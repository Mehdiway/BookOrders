using Catalog.API.Configuration;
using Catalog.API.Exceptions;
using Catalog.Infrastructure.Configuration;
using Catalog.Infrastructure.EventHandlers;
using Catalog.Infrastructure.Services;
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
    .AddInfrastructureServices();

builder.Services.AddMassTransitWithRabbitMQ(configuration, cfg =>
{
    cfg.AddConsumer<OrderPlacedEventConsumer>();
});

builder.Services.AddGrpc();

var app = builder.Build();

app.MigrateDb();

app.MapGrpcService<CatalogGrpcService>();

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
