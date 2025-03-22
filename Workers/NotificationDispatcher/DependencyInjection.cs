using Application.EntityServices.NotificationExecutions.Commands;
using Application.EntityServices.NotificationExecutions.Queries;
using Database;
using RabbitMq;

namespace NotificationDispatcher;

public static class DependencyInjection
{
    public static IServiceCollection AddDI(this IServiceCollection services)
    {
        return services
            .AddDatabase()
            .AddRepositories()
            .AddRabbitMq()
            .AddScoped<GetNotificationExecutionsQuery>()
            .AddScoped<NotificationExecutionSimpleCommands>();
    }
}