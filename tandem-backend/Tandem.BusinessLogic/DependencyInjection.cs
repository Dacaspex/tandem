using Microsoft.Extensions.DependencyInjection;
using Tandem.BusinessLogic.Topics;

namespace Tandem.BusinessLogic;

public static class DependencyInjection
{
    public static IServiceCollection AddBusinessLogic(this IServiceCollection services)
    {
        services.AddTransient<UpdateTopicBL>();

        services.AddScoped<ITopicLogic, TopicLogic>();

        return services;
    }
}