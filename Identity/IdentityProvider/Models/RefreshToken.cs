namespace IdentityProvider.Models
{
    public class RefreshToken
    {
        public Guid Id { get; set; }
        public string  Content { get; set; }
        public DateTime Expiration { get; set; }
    }
}
