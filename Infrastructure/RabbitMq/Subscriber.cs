using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace RabbitMq;

public interface ISubscriber
{
    Task StartAsync(Func<string, Task> handler, CancellationToken cancellationToken);
    Task StopAsync();
}

public class Subscriber : ISubscriber, IAsyncDisposable
{
    private IConnection? _connection;
    private IChannel? _channel;
    private AsyncEventingBasicConsumer? _consumer;
    private bool _isRunning = false;
    private string _consumerTag = string.Empty;

    public async Task StartAsync(Func<string, Task> handler, CancellationToken cancellationToken)
    {
        if (_isRunning)
        {
            return;
        }

        _consumer = new AsyncEventingBasicConsumer(_channel!);
        _consumer.ReceivedAsync += async (sender, args) =>
        {
            try
            {
                var body = args.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                await handler(message);
                await _channel!.BasicAckAsync(args.DeliveryTag, false);
            }
            catch (Exception)
            {
                await _channel!.BasicNackAsync(args.DeliveryTag, false, true);
            }
        };

        _consumerTag = await _channel!.BasicConsumeAsync(
            queue: Config.QueueName,
            autoAck: false,
            consumer: _consumer);

        _isRunning = true;
    }

    private async Task InitAsync()
    {
        if (_connection != null && _channel != null)
            return;

        var factory = new ConnectionFactory
        {
            HostName = Config.HostName,
            Port = Config.Port,
            UserName = Config.UserName,
            Password = Config.Password
        };

        _connection = await factory.CreateConnectionAsync();
        _channel = await _connection.CreateChannelAsync();

        await _channel.ExchangeDeclareAsync(
            exchange: Config.ExchangeName,
            type: ExchangeType.Direct,
            durable: true);

        await _channel.QueueDeclareAsync(
            queue: Config.QueueName,
            durable: true,
            exclusive: false,
            autoDelete: false);

        await _channel.QueueBindAsync(
            queue: Config.QueueName,
            exchange: Config.ExchangeName,
            routingKey: Config.RoutingKey);

        await _channel.BasicQosAsync(prefetchSize: 0, prefetchCount: 1, global: false);
    }

    public async Task StopAsync()
    {
        if (_isRunning == false || _channel == null || string.IsNullOrWhiteSpace(_consumerTag))
        {
            return;
        }

        await _channel.BasicCancelAsync(_consumerTag);
        _isRunning = false;
        _consumerTag = string.Empty;
    }

    public async ValueTask DisposeAsync()
    {
        if (_isRunning)
        {
            await StopAsync();
        }

        if (_channel != null)
        {
            await _channel.CloseAsync();
            _channel = null;
        }

        if (_connection != null)
        {
            await _connection.CloseAsync();
            _connection = null;
        }
    }
}