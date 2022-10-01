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
                EndPoints = { { $"{config.Url}:6380" } },
                AbortOnConnectFail = false,
                Ssl = false,
            };

           return services.AddSingleton<IConnectionMultiplexer>( ConnectionMultiplexer.Connect( redisConfig ) );
        }
    }
}
