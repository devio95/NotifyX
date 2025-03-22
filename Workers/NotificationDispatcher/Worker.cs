using Application.EntityServices.NotificationExecutions.Queries;
using Domain.Entities;

namespace NotificationDispatcher
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
            GetNotificationExecutionsQuery query = _scopeFactory.CreateScope().ServiceProvider.GetRequiredService<GetNotificationExecutionsQuery>();
            while (stoppingToken.IsCancellationRequested == false)
            {
                await Task.Delay(1000, stoppingToken);
                try
                {
                    IEnumerable<NotificationExecution> notificationExecutions = await query.GetAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.ToString());
                }
            }
        }
    }
}
