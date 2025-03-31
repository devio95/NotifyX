using Application.EntityServices.NotificationExecutions.Commands;
using Application.EntityServices.NotificationExecutions.Queries;
using Database;
using RabbitMq;

namespace NotificationDispatcher;

internal static class DependencyInjection
{
    internal static IServiceCollection AddDI(this IServiceCollection services)
    {
        return services
            .AddDatabase()
            .AddRepositories()
            .AddRabbitMq()
            .AddScoped<GetNotificationExecutionsQuery>()
            .AddScoped<NotificationExecutionSimpleCommands>();
    }
}