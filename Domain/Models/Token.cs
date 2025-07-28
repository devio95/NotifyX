namespace Domain.Models;

public class Token
{
    public string AccessToken { get; } = string.Empty;
    public string Type { get; } = string.Empty;
    public DateTime ExpiresIn { get; }
    public string Audience { get; } = string.Empty;
    public string Issuer { get; } = string.Empty;

    public Token(string accessToken, string type, DateTime expiresIn, string audience, string issuer)
    {
        AccessToken = accessToken;
        Type = type;
        ExpiresIn = expiresIn;
        Audience = audience;
        Issuer = issuer;
    }
}