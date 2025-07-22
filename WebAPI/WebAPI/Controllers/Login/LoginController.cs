using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Login;

public class LoginController : NotifyXController
{
    public LoginController(IMediator mediator)
        : base(mediator)
    { }

    [HttpPost]
    public async Task<ActionResult<int>> Token()
    {
        await Task.Delay(1);
        return 5;
    }
}
