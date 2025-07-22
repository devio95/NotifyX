namespace Application.Interfaces.Services.Auth;

public class AuthResult
{
    public string AccessToken { get; } = string.Empty;
    public string TokenType { get; } = string.Empty;
    public int ExpiresIn { get; }
    public string RefreshToken { get; } = string.Empty;

    public AuthResult(string accessToken, string tokenType, int expiresIn, string refreshToken)
    {
        AccessToken = accessToken;
        TokenType = tokenType;
        ExpiresIn = expiresIn;
        RefreshToken = refreshToken;
    }
}