using Application.EntityServices.NotificationExecutions.Commands;
using Application.Functionalities.NotificationExecutions.Commands;
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
    public async Task<ActionResult> StartProcessing(long notificationExecutionId)
    {
        await _mediator.Send(new NotificationExecutionStartProcessingCommand(notificationExecutionId));
        return Ok();
    }

    [HttpPost("send")]
    public async Task<ActionResult> Send(long notificationExecutionId)
    {
        await _mediator.Send(new NotificationExecutionSendCommand(notificationExecutionId));
        return Ok();
    }
}
