using Application.Interfaces.Services;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace RabbitMq;


public class Publisher : IMessagePublisher, IAsyncDisposable, IDisposable
{
    private IConnection? _connection;
    private IChannel? _channel;

    public async Task SendAsync(object obj)
    {
        await InitAsync();
        string message = JsonSerializer.Serialize(obj);
        var body = Encoding.UTF8.GetBytes(message);

        await _channel!.BasicPublishAsync(Config.ExchangeName, Config.RoutingKey, body);
    }

    private async Task InitAsync()
    {
        if (_connection == null || _channel == null)
        {
            var factory = new ConnectionFactory
            {
                HostName = Config.HostName,
                Port = Config.Port,
                UserName = Config.UserName,
                Password = Config.Password
            };

            _connection = await factory.CreateConnectionAsync();
            _channel = await _connection.CreateChannelAsync();
            await _channel!.ExchangeDeclareAsync(
               exchange: Config.ExchangeName,
               type: ExchangeType.Direct,
               durable: true);

            await _channel!.QueueDeclareAsync(
                queue: Config.QueueName,
                durable: true,
                exclusive: false,
                autoDelete: false);

            await _channel.QueueBindAsync(
                queue: Config.QueueName,
                exchange: Config.ExchangeName,
                routingKey: Config.RoutingKey);
        }
    }

    public async ValueTask DisposeAsync()
    {
        await _channel?.CloseAsync();
        await _connection?.CloseAsync();
    }

    public void Dispose()
    {
        try
        {
            _channel?.CloseAsync().GetAwaiter().GetResult();
            _connection?.CloseAsync().GetAwaiter().GetResult();
        }
        catch
        {
        }
    }
}