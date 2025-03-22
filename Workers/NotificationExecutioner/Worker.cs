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

        private Task ProcessMessageAsync(string message)
        {
            throw new NotImplementedException();
            try
            {
                _logger.LogInformation("ODEBRANO WIADOMOSC");
                _logger.LogInformation(message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }
        }
    }
}
