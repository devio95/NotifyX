using Application.EntityServices.NotificationExecutions;
using Database;

namespace NotifyCalculator;

public static class DependencyInjection
{
    public static IServiceCollection AddDI(this IServiceCollection services)
    {
        return services
            .AddDatabase()
            .AddRepositories()
            .AddScoped<IGenerateNotificationExecutions, GenerateNotificationExecutions>();
    }
}