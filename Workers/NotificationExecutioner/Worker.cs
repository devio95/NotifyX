using Application.Messages.Functionalities;
using MediatR;

namespace NotificationExecutioner
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IMediator _mediator;
        public Worker(ILogger<Worker> logger, IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
            _mediator = _scopeFactory.CreateScope().ServiceProvider.GetRequiredService<IMediator>();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                await Task.Delay(5000);
                await _mediator.Send(new StartMessageSubscribtionCommand());
                while (stoppingToken.IsCancellationRequested == false)
                {
                    await Task.Delay(1000, stoppingToken);
                }
                await _mediator.Send(new StopMessageSubscribtionCommand());
            }
            catch (Exception ex)
            {
                await _mediator.Send(new StopMessageSubscribtionCommand());
                _logger.LogError(ex.ToString());
            }
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await _mediator.Send(new StopMessageSubscribtionCommand());
            await base.StopAsync(cancellationToken);
        }
    }
}
