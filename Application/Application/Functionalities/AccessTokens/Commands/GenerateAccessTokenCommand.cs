using Application.DTO.AccessTokens;
using Application.Exceptions;
using Application.Interfaces.Services.Auth;
using MediatR;

namespace Application.EntityServices.OAuthTokens.Commands;

public class GenerateAccessTokenCommand : IRequest<GenerateAccessTokenCommandResponse>
{ 
    public string Username { get; }
    public string Password { get; }

    public GenerateAccessTokenCommand(string username, string password)
    {
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            throw new DataValidationException();
        }

        Username = username;
        Password = password;
    }
}

public class GenerateAccessTokenCommandHandler(IAuthService authService)
    : IRequestHandler<GenerateAccessTokenCommand, GenerateAccessTokenCommandResponse>
{
    private readonly IAuthService _authService = authService;
    public async Task<GenerateAccessTokenCommandResponse> Handle(GenerateAccessTokenCommand request, CancellationToken cancellationToken)
    {
        AuthResult authResult = await _authService.AuthenticateAsync(request.Username, request.Password);
        return new GenerateAccessTokenCommandResponse(new GenerateAccessTokenDto(
            accessToken: authResult.AccessToken,
            tokenType: authResult.TokenType,
            expiresIn: authResult.ExpiresIn,
            refreshToken: authResult.RefreshToken));
    }
}