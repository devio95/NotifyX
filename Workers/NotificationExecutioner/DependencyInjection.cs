using Application;
using Application.Messages;
using Database;
using Infrastructure;
using RabbitMq;

namespace NotificationExecutioner;

internal static class DependencyInjection
{
    internal static IServiceCollection AddDI(this IServiceCollection services)
    {
        return services
            .AddApplication()
            .AddApplicationMessages()
            .AddDatabase()
            .AddRepositories()
            .AddRabbitMqSingleton()
            .AddInfrastructure();
    }
}