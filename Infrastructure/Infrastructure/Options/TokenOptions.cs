using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Options;

public class TokenOptions
{
    public const string Name = "Token";

    [Required]
    public int ExpiresIn { get; set; } = 60;

    [Required]
    [MinLength(1)]
    public string Audience { get; set; } = string.Empty;

    [Required]
    [MinLength(1)]
    public string Issuer { get; set; } = string.Empty;

    [Required]
    [MinLength(10)]
    public string Key { get; set; } = string.Empty;
}