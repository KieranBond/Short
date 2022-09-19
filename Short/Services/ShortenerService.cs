using CQRS.Commands;
using StackExchange.Redis;

namespace Short.Services
{
    public class ShortenerService
    {
        private readonly IDatabase _redis;

        public ShortenerService( IConnectionMultiplexer redis )
        {
            _redis = redis.GetDatabase();
        }

        public string ShortenUrl( Handle<string> cmd )
        {
            var dto = cmd.Dto;
            
            if ( _redis.KeyExists(dto) )
            {
                return _redis.StringGet( dto );
            }

            var shortenedUrl = Guid.NewGuid().ToString()[ ..5 ];
            _redis.StringSet( dto, shortenedUrl, TimeSpan.FromHours(24) );
            return shortenedUrl;
        }
    }
}
