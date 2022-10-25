using Short.Config;
using StackExchange.Redis;

namespace Short.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRedis( this IServiceCollection services, RedisConfig config )
        {
            ConfigurationOptions redisConfig = new()
            {
                EndPoints = { $"{config.Url}:6379" },
                AbortOnConnectFail = true,
                //AbortOnConnectFail = false,
                Ssl = false,
            };

           return services.AddSingleton<IConnectionMultiplexer>( ConnectionMultiplexer.Connect( redisConfig ) );
        }
    }
}
