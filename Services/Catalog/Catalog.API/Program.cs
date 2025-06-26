using Catalog.API.Configuration;
using Catalog.API.Exceptions;
using Catalog.Infrastructure.Configuration;
using Catalog.Infrastructure.EventsConsumers;
using Shared.Messaging.Configuration;

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

builder.Services.AddMassTransitWithRabbitMq(configuration, cfg =>
{
    cfg.AddConsumer<OrderPlacedConsumer>();
});

var app = builder.Build();

app.MigrateDb();

app.UseMiddleware<GlobalExceptionHandler>();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
