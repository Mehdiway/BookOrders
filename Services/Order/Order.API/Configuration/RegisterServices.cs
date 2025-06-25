using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Order.API.PipelineBehaviors;
using Order.Infrastructure;
using Shared.PipelineBehaviors;
using System.Reflection;

namespace Order.API.Configuration;

public static class RegisterServices
{
    public static IServiceCollection AddEFCore(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<OrderContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });
        return services;
    }

    public static IServiceCollection AddMediatR(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });
        return services;
    }

    public static IServiceCollection AddFluentValidation(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(Program).Assembly);
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnitOfWorkBehavior<,>));
        return services;
    }

    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }

    public static IServiceCollection AddHttpClients(this IServiceCollection services)
    {
        services.AddHttpClient("catalog", client =>
        {
            client.BaseAddress = new Uri("https://localhost:7000");
        });
        return services;
    }
}
