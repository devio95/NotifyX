using Application.Interfaces.Services.Messages;
using Microsoft.Extensions.DependencyInjection;

namespace RabbitMq;

public static class DependencyInjection
{
    public static IServiceCollection AddRabbitMqSingleton(this IServiceCollection services)
    {
        return services
            .AddSingleton<IMessageSubscriber, Subscriber>()
            .AddSingleton<IMessagePublisher, Publisher>();
    }
}