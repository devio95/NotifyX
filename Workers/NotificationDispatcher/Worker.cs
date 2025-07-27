using Application.Interfaces;
using Application.Messages.Functionalities;
using MediatR;

namespace NotificationDispatcher
{
    public class Worker : BackgroundService
    {
        private readonly ILoggingManager<Worker> _logger;
        private readonly IServiceScopeFactory _scopeFactory;
        
        public Worker(ILoggingManager<Worker> logger, IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (await WaitForServerAsync(stoppingToken) == false)
            {
                _logger.LogError("No connection with Messages Server");
                return;
            }

            _logger.LogInformation("Connection with Messages Server OK");
            while (stoppingToken.IsCancellationRequested == false)
            {
                await Task.Delay(1000, stoppingToken);
                try
                {
                    using var scope = _scopeFactory.CreateScope();
                    var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                    await mediator.Send(new PublishMessagesCommand());
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.ToString());
                }
            }
        }

        private async Task<bool> WaitForServerAsync(CancellationToken stoppingToken)
        {
            using var scope = _scopeFactory.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            return await mediator.Send(new WaitForMessagesServerCommand());
        }
    }
}
