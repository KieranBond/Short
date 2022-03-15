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
            throw new NotImplementedException();
            // TODO: See if URL is cached
            // var shortenedUrl = Compress( url );
            // SaveUrl( url, shortenedUrl );
        }

        private string Compress( string toCompress )
        {
            throw new NotImplementedException();
        }
    }
}
