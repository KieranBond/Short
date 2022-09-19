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

            // TODO: We can't guarantee currently that this value is unique...
            var shortenedUrl = Guid.NewGuid().ToString()[ ..5 ];
            _redis.StringSet( dto, shortenedUrl, TimeSpan.FromHours(24) );
            return shortenedUrl;
        }
    }
}
