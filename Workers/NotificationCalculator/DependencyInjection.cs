using Application.EntityServices.NotificationExecutions.Commands;
using Application.EntityServices.Notifications;
using Database;

namespace NotifyCalculator;

public static class DependencyInjection
{
    public static IServiceCollection AddDI(this IServiceCollection services)
    {
        return services
            .AddDatabase()
            .AddRepositories()
            .AddScoped<GenerateNextNotificationExecutionsCommand>()
            .AddScoped<GetNotificationsQueries>();
    }
}