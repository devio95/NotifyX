using Application.EntityServices.NotificationExecutions.Commands;
using Application.EntityServices.Notifications;
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
            await _subscriber.StartAsync(ProcessMessageAsync, stoppingToken);
            while (stoppingToken.IsCancellationRequested == false)
            {
                await Task.Delay(1000, stoppingToken);
            }
            await _subscriber.StopAsync();
        }

        private async Task ProcessMessageAsync(string message)
        {
            NotificationExecutionSimpleCommands finishNotificationExecutionCommand = _scopeFactory.CreateScope().ServiceProvider.GetRequiredService<NotificationExecutionSimpleCommands>();
            GenerateNextNotificationExecutionsCommand generateNextNotificationCommand = _scopeFactory.CreateScope().ServiceProvider.GetRequiredService<GenerateNextNotificationExecutionsCommand>();
            GetNotificationsQueries getNotificationsQueries = _scopeFactory.CreateScope().ServiceProvider.GetRequiredService<GetNotificationsQueries>();
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

                await finishNotificationExecutionCommand.FinishOkAsync(notification.NextNotificationExecutionId.Value);
                await generateNextNotificationCommand.GenerateNextNotificationExecutionAsync(notificationId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }
        }
    }
}
