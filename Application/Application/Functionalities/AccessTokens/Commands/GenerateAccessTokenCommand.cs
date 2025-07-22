using Application.Interfaces.Services.Auth;

namespace Application.EntityServices.OAuthTokens.Commands;

public class GenerateAccessTokenCommand(IAuthService authService)
{ 
    private readonly IAuthService _authService = authService;

    //public async Task<AccessTokenResponse> GenerateAsync(string clientId, string clientSecret)
    //{
       
    //}

    //private string Assert(string clientId, string clientSecret)
    //{
    //    if (string.IsNullO)
    //}
}