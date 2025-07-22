using Application.Interfaces.Services.Auth;
using Keycloak.Options;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Keycloak;

public class KeycloakAuthService(IHttpClientFactory httpClientFactory, IOptions<KeycloakOptions> keycloakOptions)
    : IAuthService
{
    private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
    private readonly KeycloakOptions _keycloakOptions = keycloakOptions.Value;
    public async Task<AuthResult> AuthenticateAsync(string clientId, string clientSecret)
    {
        try
        {
            HttpClient client = _httpClientFactory.CreateClient(KeycloakOptions.HttpClientName);
            string endpoint = $"realms/{_keycloakOptions.Realm}/protocol/openid-connect/token";

            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("client_id", _keycloakOptions.ClientId),
                new KeyValuePair<string, string>("username", clientId),
                new KeyValuePair<string, string>("password", clientSecret),
            });

            var response = await client.PostAsync(endpoint, content);
            if (response.IsSuccessStatusCode == false)
            {
                throw new UnauthorizedAccessException("Failed to authenticate");
            }

            var responseString = await response.Content.ReadAsStringAsync();
            var keycloakResponse = JsonSerializer.Deserialize<KeycloakTokenResponse>(responseString, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
            });

            return new AuthResult(
                accessToken: keycloakResponse?.AccessToken ?? string.Empty,
                tokenType: keycloakResponse?.TokenType ?? "Bearer",
                expiresIn: keycloakResponse?.ExpiresIn ?? 0,
                refreshToken: keycloakResponse?.RefreshToken ?? string.Empty);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    internal class KeycloakTokenResponse
    {
        public string AccessToken { get; set; } = string.Empty;
        public string TokenType { get; set; } = string.Empty;
        public int ExpiresIn { get; set; }
        public string RefreshToken { get; set; } = string.Empty;
        public string Scope { get; set; } = string.Empty;
    }
}