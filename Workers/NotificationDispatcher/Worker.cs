using Application.DTO.NotificationExecutions;
using Application.EntityServices.NotificationExecutions.Commands;
using Application.EntityServices.NotificationExecutions.Queries;
using Application.Functionalities.NotificationExecutions.Queries;
using MediatR;

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
            while (stoppingToken.IsCancellationRequested == false)
            {
                await Task.Delay(1000, stoppingToken);
                try
                {
                    using var scope = _scopeFactory.CreateScope();
                    var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                    NotificationExecutionsGetFilteredResponse notificationExecutions = await mediator.Send(new NotificationExecutionsGetFilteredQuery(5));

                    if (notificationExecutions.Response.Any())
                    {
                        LogNotificationsToDispatch(notificationExecutions.Response);
                    }

                    foreach (NotificationExecutionsGetFilteredDto notificationExecution in notificationExecutions.Response)
                    {
                        await mediator.Send(new NotificationExecutionStartProcessingCommand(notificationExecution.Id));
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.ToString());
                }
            }
        }

        private void LogNotificationsToDispatch(IEnumerable<NotificationExecutionsGetFilteredDto> notificationExecutions)
        {
            string log = $"Notifications to dispatch: {notificationExecutions.Count()}{Environment.NewLine}";
            log += $"{string.Join(",", notificationExecutions.Select(x => x.Id.ToString()))}";
            _logger.LogInformation(log);
        }
    }
}
