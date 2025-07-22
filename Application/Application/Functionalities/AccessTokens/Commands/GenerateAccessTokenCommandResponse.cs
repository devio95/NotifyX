using Application.DTO.AccessTokens;

namespace Application.EntityServices.OAuthTokens.Commands;

public class GenerateAccessTokenCommandResponse
{
    public GenerateAccessTokenDto Response { get; }
    public GenerateAccessTokenCommandResponse(GenerateAccessTokenDto response)
    {
        Response = response;
    }
}