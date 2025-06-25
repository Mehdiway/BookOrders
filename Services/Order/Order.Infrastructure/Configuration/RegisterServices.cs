using Microsoft.Extensions.DependencyInjection;
using Order.Domain.Repositories;
using Order.Domain.Services;
using Order.Infrastructure.Profiles;
using Order.Infrastructure.Repositories;
using Order.Infrastructure.Services;

namespace Order.Infrastructure.Configuration;
public static class RegisterServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MappingProfile).Assembly);
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IOrderItemRepository, OrderItemRepository>();
        return services;
    }
}
