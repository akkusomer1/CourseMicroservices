namespace IdentityProvider.Settings
{
    public interface ICustomTokenOptions
    {
        string[] Audiences { get; set; }
        string Issuer { get; set; }
        int AccessTokenExpiration { get; set; }
        int RefreshTokenExpiration { get; set; }
        string SecurityKey { get; set; }
    }
}
