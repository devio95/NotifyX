using Application.EntityServices.NotificationExecutions.Commands;
using Application.EntityServices.Notifications;
using Database;
using RabbitMq;

namespace NotificationExecutioner;

public static class DependencyInjection
{
    public static IServiceCollection AddDI(this IServiceCollection services)
    {
        return services
            .AddDatabase()
            .AddRepositories()
            .AddRabbitMq()
            .AddScoped<NotificationExecutionSimpleCommands>()
            .AddScoped<GenerateNextNotificationExecutionsCommand>()
            .AddScoped<GetNotificationsQueries>();
    }
}