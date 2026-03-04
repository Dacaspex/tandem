using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;
using Tandem.Persistence.Interfaces;
using Tandem.Persistence.Repositories;

namespace Tandem.Persistence;

public static class DependencyInjection
{
    private const string DbConnectionString = "DefaultConnection";

    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<TandemContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString(DbConnectionString), ConfigurePostgres);
        });

        services.AddTransient<UserRepository>();
        services.AddScoped<ITopicRepository, TopicRepository>();

        return services;
    }

    private static void ConfigurePostgres(NpgsqlDbContextOptionsBuilder options)
    {
        options.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(10),
            errorCodesToAdd: null);
    }
}