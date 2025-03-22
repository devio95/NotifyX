using Application.EntityServices.NotificationExecutions.Commands;
using Database;

namespace NotifyCalculator;

public static class DependencyInjection
{
    public static IServiceCollection AddDI(this IServiceCollection services)
    {
        return services
            .AddDatabase()
            .AddRepositories()
            .AddScoped<GenerateNotificationExecutionsCommand>();
    }
}