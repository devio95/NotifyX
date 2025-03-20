using Application.EntityServices.NotificationExecutions;

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
            while (stoppingToken.IsCancellationRequested == false)
            {
                try
                {
                    await Task.Delay(1000, stoppingToken);
                    IGenerateNotificationExecutions generateExecutions = _scopeFactory.CreateScope()
                        .ServiceProvider.GetRequiredService<IGenerateNotificationExecutions>();

                    await generateExecutions.GenerateAsync();
                }
                catch (Exception ex)
                {

                }
            }
        }
    }
}
