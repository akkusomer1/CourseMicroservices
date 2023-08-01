namespace CourseMicroservices.Services.Basket.Settings
{
    public interface IRedisSetttings
    {
        string Host { get; set; }
        string Port { get; set; }
    }
}
