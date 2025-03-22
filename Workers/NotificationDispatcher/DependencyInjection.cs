using Application.EntityServices.NotificationExecutions.Queries;
using Database;

namespace NotificationDispatcher;

public static class DependencyInjection
{
    public static IServiceCollection AddDI(this IServiceCollection services)
    {
        return services
            .AddDatabase()
            .AddRepositories()
            .AddScoped<GetNotificationExecutionsQuery>();
    }
}