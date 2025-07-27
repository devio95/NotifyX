using Application.Interfaces;
using Application.Messages.Functionalities;
using MediatR;

namespace NotificationExecutioner
{
    public class Worker : BackgroundService
    {
        private readonly ILoggingManager<Worker> _logger;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IMediator _mediator;
        public Worker(ILoggingManager<Worker> logger, IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
            _mediator = _scopeFactory.CreateScope().ServiceProvider.GetRequiredService<IMediator>();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (await WaitForServerAsync(stoppingToken) == false)
            {
                _logger.LogError("No connection with Messages Server");
                return;
            }

            _logger.LogInformation("Connection with Messages Server OK");
            try
            {
                await _mediator.Send(new StartMessageSubscribtionCommand());
                while (stoppingToken.IsCancellationRequested == false)
                {
                    await Task.Delay(1000, stoppingToken);
                }
                await _mediator.Send(new StopMessageSubscribtionCommand());
                return;
            }
            catch (Exception ex)
            {
                await _mediator.Send(new StopMessageSubscribtionCommand());
                _logger.LogException(ex);
            }
        }

        private async Task<bool> WaitForServerAsync(CancellationToken stoppingToken)
        {
            using var scope = _scopeFactory.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            return await mediator.Send(new WaitForMessagesServerCommand(), stoppingToken);
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await _mediator.Send(new StopMessageSubscribtionCommand());
            await base.StopAsync(cancellationToken);
        }
    }
}
