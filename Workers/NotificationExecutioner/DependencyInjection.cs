using Application.EntityServices.NotificationExecutions.Commands;
using Application.EntityServices.Notifications.Commands;
using Application.EntityServices.Notifications.Queries;
using Database;
using RabbitMq;

namespace NotificationExecutioner;

internal static class DependencyInjection
{
    internal static IServiceCollection AddDI(this IServiceCollection services)
    {
        return services
            .AddDatabase()
            .AddRepositories()
            .AddRabbitMq()
            .AddScoped<NotificationExecutionSimpleCommands>()
            .AddScoped<ProcessNotificationCommand>()
            .AddScoped<GetNotificationsQueries>();
    }
}