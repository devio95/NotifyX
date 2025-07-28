using Application.Interfaces;
using Application.Interfaces.Repositories;
using Database.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Database;

public static class DependencyInjection
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        return services
            .AddScoped<INotificationRepository, NotificationRepository>()
            .AddScoped<INotificationExecutionRepository, NotificationExecutionRepository>()
            .AddScoped<IUserRepository, UserRepository>()
            .AddScoped<IUserAuthProviderRepository, UserAuthProviderRepository>()
            .AddScoped<IUnitOfWork, UnitOfWork>();
    }
}