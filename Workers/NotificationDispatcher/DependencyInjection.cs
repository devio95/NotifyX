using Application;
using Application.Messages;
using Database;
using Keycloak;
using RabbitMq;

namespace NotificationDispatcher;

internal static class DependencyInjection
{
    internal static IServiceCollection AddDI(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddApplication()
            .AddApplicationMessages()
            .AddDatabase()
            .AddRepositories()
            .AddRabbitMqSingleton();
    }
}