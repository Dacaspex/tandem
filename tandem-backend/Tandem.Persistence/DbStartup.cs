using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Tandem.Persistence;

public class DbStartup
{
    public static void ProvisionSchema(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<TandemContext>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<DbStartup>>();
        logger.LogInformation("Migrating database");
        db.Database.EnsureCreated();
    }
}