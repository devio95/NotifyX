using Application.EntityServices.OAuthTokens.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Login;

public class LoginController : NotifyXController
{
    public LoginController(IMediator mediator)
        : base(mediator)
    { }

    [HttpPost]
    public async Task<ActionResult<GenerateAccessTokenCommandResponse>> Token([FromForm(Name = "client_id")] string clientId, [FromForm(Name = "client_secret")] string clientSecret)
    {
        GenerateAccessTokenCommandResponse response = await _mediator.Send(new GenerateAccessTokenCommand(clientId, clientSecret));
        return response;
    }
}
