using Microsoft.EntityFrameworkCore;
using Order.API.Configuration;
using Order.Infrastructure;
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
    .AddSwagger()
    .AddInfrastructureServices()
    .AddHttpClients();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<OrderContext>();
    db.Database.Migrate();
}

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
