using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Tandem.Infrastructure.Database;

public class DbStartup
{
    public static Task MigrateAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<TandemContext>();
        var logger  = scope.ServiceProvider.GetRequiredService<ILogger<DbStartup>>();
        logger.LogInformation("Migrating database");
        return db.Database.MigrateAsync();
    }
}