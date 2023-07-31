namespace IdentityProvider.Settings
{
    public class CustomTokenOptions: ICustomTokenOptions
    {
        public string[] Audiences { get; set; }
        public string Issuer { get; set; }
        public int AccessTokenExpiration { get; set; }
        public int RefreshTokenExpiration { get; set; }
        public string SecurityKey { get; set; }
    }
}
