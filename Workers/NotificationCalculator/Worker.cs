using Application.EntityServices.NotificationExecutions.Commands;
using Application.EntityServices.NotificationExecutions.Queries;
using Application.EntityServices.Notifications;
using Domain.Entities;

namespace NotifyCalculator
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceScopeFactory _scopeFactory;
        public Worker(ILogger<Worker> logger, IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            GenerateNextNotificationExecutionsCommand generateExecutions = _scopeFactory.CreateScope().ServiceProvider.GetRequiredService<GenerateNextNotificationExecutionsCommand>();
            GetNotificationsQueries notificationQueries = _scopeFactory.CreateScope().ServiceProvider.GetRequiredService<GetNotificationsQueries>();
            while (stoppingToken.IsCancellationRequested == false)
            {
                await Task.Delay(1000, stoppingToken);
                try
                {
                    Notification? notification = await notificationQueries.GetOneAsync(2);
                    if (notification == null)
                    {
                        continue;
                    }

                    //await generateExecutions.GenerateNextNotificationExecutionAsync(notification);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.ToString());
                }
            }
        }
    }
}
