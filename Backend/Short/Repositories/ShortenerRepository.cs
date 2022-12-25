using StackExchange.Redis;

namespace Short.Repositories
{
    public sealed class ShortenerRepository : IShortenerRepository
    {
        private readonly IDatabase _redis;

        public ShortenerRepository ( IConnectionMultiplexer redis )
        {
            _redis = redis.GetDatabase();
        }

        public bool Save ( string shortenedUrl, string originalUrl )
        {
            return _redis.StringSet( originalUrl, shortenedUrl, TimeSpan.FromHours( 24 ) );
        }

        public bool TryRetrieve ( string urlToQuery, out string? retrievedUrl)
        {
            if( _redis.KeyExists( urlToQuery ) )
            {
                retrievedUrl = _redis.StringGet( urlToQuery );
                return true;
            }

            retrievedUrl = null;
            return false;
        }
    }
}
