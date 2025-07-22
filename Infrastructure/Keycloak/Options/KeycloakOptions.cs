using System.ComponentModel.DataAnnotations;

namespace Keycloak.Options;

public class KeycloakOptions
{
    public const string Name = "Keycloak";
    public const string HttpClientName = "KeycloakHttpClient";

    [Required]
    [Url]
    public string Address { get; set; } = string.Empty;

    [Required]
    [MinLength(1)]
    public string Realm { get; set; } = string.Empty;

    [Required]
    [MinLength(1)]
    public string ClientId { get; set; } = string.Empty;
}