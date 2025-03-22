using Application.EntityServices.NotificationExecutions.Commands;

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
            GenerateNotificationExecutionsCommand generateExecutions = _scopeFactory.CreateScope().ServiceProvider.GetRequiredService<GenerateNotificationExecutionsCommand>();

            while (stoppingToken.IsCancellationRequested == false)
            {
                await Task.Delay(1000, stoppingToken);
                try
                {
                    await generateExecutions.GenerateAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.ToString());
                }
            }
        }
    }
}
