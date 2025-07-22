using Application;
using Database;
using RabbitMq;

namespace NotificationExecutioner;

internal static class DependencyInjection
{
    internal static IServiceCollection AddDI(this IServiceCollection services)
    {
        return services
            .AddApplication()
            .AddDatabase()
            .AddRepositories()
            .AddRabbitMq();
    }
}