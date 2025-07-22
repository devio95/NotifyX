using Application.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;

namespace RabbitMq;

public static class DependencyInjection
{
    public static IServiceCollection AddRabbitMq(this IServiceCollection services)
    {
        return services
            .AddScoped<IMessagePublisher, Publisher>()
            .AddScoped<ISubscriber, Subscriber>();
    }
}