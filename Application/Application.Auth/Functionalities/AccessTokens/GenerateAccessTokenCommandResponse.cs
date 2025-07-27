using Application.Auth.DTO;

namespace Application.Auth.AccessTokens.Commands;

public class GenerateAccessTokenCommandResponse
{
    public GenerateAccessTokenDto Response { get; }
    public GenerateAccessTokenCommandResponse(GenerateAccessTokenDto response)
    {
        Response = response;
    }
}