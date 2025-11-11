using Microsoft.Extensions.DependencyInjection;
using Tandem.Persistence.Repositories;

namespace Tandem.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        services.AddTransient<UserRepository>();

        return services;
    }
}