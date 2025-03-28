using Application.EntityServices.NotificationExecutions.Commands;
using Application.EntityServices.Notifications.Commands;
using Application.EntityServices.Notifications.Queries;
using Domain.Entities;
using RabbitMq;

namespace NotificationExecutioner
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ISubscriber _subscriber;
        public Worker(ILogger<Worker> logger, IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
            _subscriber = _scopeFactory.CreateScope().ServiceProvider.GetRequiredService<ISubscriber>();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Delay(5000);
            await _subscriber.StartAsync(ProcessMessageAsync, stoppingToken);
            while (stoppingToken.IsCancellationRequested == false)
            {
                await Task.Delay(1000, stoppingToken);
            }
            await _subscriber.StopAsync();
        }

        private async Task ProcessMessageAsync(string message)
        {
            _logger.LogInformation($"Message to process{Environment.NewLine}{message}");
            IServiceProvider provider = _scopeFactory.CreateScope().ServiceProvider;
            NotificationExecutionSimpleCommands finishNotificationExecutionCommand = provider.GetRequiredService<NotificationExecutionSimpleCommands>();
            GenerateNextNotificationExecutionsCommand generateNextNotificationCommand = provider.GetRequiredService<GenerateNextNotificationExecutionsCommand>();
            GetNotificationsQueries getNotificationsQueries = provider.GetRequiredService<GetNotificationsQueries>();
            NotificationsSimpleCommands notificationsSimpleCommands = provider.GetRequiredService<NotificationsSimpleCommands>();


            try
            {
                if (int.TryParse(message, out var notificationId) == false)
                {
                    return;
                }

                Notification? notification = await getNotificationsQueries.GetOneAsync(notificationId);
                if (notification == null)
                {
                    return;
                }

                if (notification.NextNotificationExecutionId == null)
                {
                    return;
                }

                long nextNotificationExecutionId = notification.NextNotificationExecutionId.Value;
                await notificationsSimpleCommands.ClearNextNotificationExecutionAsync(notification.Id);
                await finishNotificationExecutionCommand.FinishOkAsync(nextNotificationExecutionId);
                await generateNextNotificationCommand.GenerateNextNotificationExecutionAsync(notificationId);

                _logger.LogInformation($"Message processed OK");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Message processing FAILED{Environment.NewLine}{ex.ToString()}");
            }
        }
    }
}
