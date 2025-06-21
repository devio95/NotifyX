namespace Application.Interfaces.Services.Auth;

public interface IAuthService
{
    Task<AuthResult> AuthenticateAsync(string clientId, string clientSecret);
}