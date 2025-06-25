using Catalog.Domain.Repositories;
using Catalog.Domain.Services;
using Catalog.Infrastructure.Profiles;
using Catalog.Infrastructure.Repositories;
using Catalog.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Infrastructure.Configuration;
public static class ServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MappingProfile).Assembly);
        services.AddScoped<IBookService, BookService>();
        services.AddScoped<IBookRepository, BookRepository>();
        return services;
    }
}
