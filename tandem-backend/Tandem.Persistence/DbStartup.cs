using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Tandem.Persistence;

public static class Startup
{
    public static void ProvisionSchema(IServiceProvider serviceProvider)
    {
        var db = serviceProvider.GetRequiredService<TandemContext>();
        db.Database.Migrate();
    }
}