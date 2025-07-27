using Application.Messages.Functionalities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/notification-executions")]
public class NotificationExecutionsController : NotifyXController
{
    public NotificationExecutionsController(IMediator mediator)
        : base(mediator)
    { }

    [HttpPost("start-processing")]
    public async Task<ActionResult> StartProcessing(string notificationExecutionId)
    {
        await _mediator.Send(new ExecuteMessageCommand(notificationExecutionId));
        return Ok();
    }

    [HttpPost("send")]
    public async Task<ActionResult> Send(long notificationExecutionId)
    {
        await _mediator.Send(new PublishMessageCommand(notificationExecutionId));
        return Ok();
    }
}
