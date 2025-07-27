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
            try
            {
                await TrySubscribeAsync(stoppingToken);
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

        private async Task TrySubscribeAsync(CancellationToken stoppingToken)
        {
            for (int i = 1; i <= 10; i++)
            {
                try
                {
                    await _mediator.Send(new StartMessageSubscribtionCommand());
                    return;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Execute Async {i}/10");
                    await Task.Delay(2000, stoppingToken);
                    continue;
                }
            }

            throw new Exception("Unable to subscribe");
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await _mediator.Send(new StopMessageSubscribtionCommand());
            await base.StopAsync(cancellationToken);
        }
    }
}
