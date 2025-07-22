namespace Application.DTO.AccessTokens;

public class GenerateAccessTokenDto
{
    public string AccessToken { get; }
    public string TokenType { get; }
    public int ExpiresIn { get; }
    public string RefreshToken { get; }

    public GenerateAccessTokenDto(string accessToken, string tokenType, int expiresIn, string refreshToken)
    {
        AccessToken = accessToken;
        TokenType = tokenType;
        ExpiresIn = expiresIn;
        RefreshToken = refreshToken;
    }
}