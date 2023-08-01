namespace CourseMicroservices.Services.Basket.Settings
{
    public class RedisSetttings: IRedisSetttings
    {
        public string Host { get; set; }
        public string Port { get; set; }
    }
}
