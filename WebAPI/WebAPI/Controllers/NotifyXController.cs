using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotifyXController : ControllerBase
{
    protected readonly IMediator _mediator;

    public NotifyXController(IMediator mediator)
    {
        _mediator = mediator;
    }
}