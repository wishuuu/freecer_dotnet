namespace Freecer.Domain.Configs;

public class AuthConfig
{
    public string Secret { get; set; } = string.Empty;
    public int ExpiresInMinutes { get; set; }
    public int RefreshTokenExpiresInMinutes { get; set; }
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
}