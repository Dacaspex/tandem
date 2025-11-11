using Microsoft.Extensions.DependencyInjection;

namespace Tandem.BusinessLogic;

public static class DependencyInjection
{
    public static IServiceCollection AddBusinessLogic(this IServiceCollection services)
    {
        services.AddTransient<UpdateTopicBL>();

        return services;
    }
}