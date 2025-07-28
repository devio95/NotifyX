namespace Application.Interfaces.Services.Auth;

public interface IExternalAuthService
{
    Task<ExternalAuthResult> AuthenticateAsync(string clientId, string clientSecret);
}