using Application.Functionalities.NotificationExecutions.Commands;
using MediatR;
using RabbitMq;

namespace NotificationExecutioner
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly ISubscriber _subscriber;
        private readonly IServiceScopeFactory _scopeFactory;
        public Worker(ILogger<Worker> logger, IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
            _subscriber = _scopeFactory.CreateScope().ServiceProvider.GetRequiredService<ISubscriber>();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                string host = RabbitMq.Config.HostName;
                _logger.LogInformation("HOST: " + host);
                await Task.Delay(5000);
                await _subscriber.StartAsync(ProcessMessageAsync, stoppingToken);
                while (stoppingToken.IsCancellationRequested == false)
                {
                    await Task.Delay(1000, stoppingToken);
                }
                await _subscriber.StopAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }
        }

        private async Task ProcessMessageAsync(string message)
        {
            _logger.LogInformation($"Message to process{Environment.NewLine}{message}");

            try
            {
                using var scope = _scopeFactory.CreateScope();
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                if (int.TryParse(message, out var notificationExecutionId) == false)
                {
                    return;
                }

                await mediator.Send(new NotificationExecutionSendCommand(notificationExecutionId));

                _logger.LogInformation($"Message processed OK");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Message processing FAILED{Environment.NewLine}{ex.ToString()}");
            }
        }
    }
}
