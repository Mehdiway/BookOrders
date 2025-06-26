using Grpc.Net.Client;
using Order.API.Configuration;
using Order.API.Exceptions;
using Order.Infrastructure.Configuration;

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

// Configure gRPC client
var catalogServiceUrl = builder.Configuration.GetConnectionString("CatalogService");
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
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
