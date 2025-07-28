namespace Application.Auth.DTO;

public class GenerateAccessTokenDto
{
    public string AccessToken { get; }
    public string TokenType { get; }
    public DateTime ExpiresIn { get; }
    public string Audience { get; }
    public string Issuer { get; }

    public GenerateAccessTokenDto(string accessToken, string tokenType, DateTime expiresIn, string audience, string issuer)
    {
        AccessToken = accessToken;
        TokenType = tokenType;
        ExpiresIn = expiresIn;
        Audience = audience;
        Issuer = issuer;
    }
}