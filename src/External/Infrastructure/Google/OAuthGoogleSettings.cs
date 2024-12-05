namespace Infrastructure.Google;

public class OAuthGoogleSettings
{
    public string? ClientId { get; set; }
    public string? ClientSecret { get; set; }
    public string? RedirectUri { get; set; }
    public List<string> Scopes { get; set; } = new List<string>();
}