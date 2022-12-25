namespace Short.Repositories
{
    public interface IShortenerRepository
    {
        bool TryRetrieve ( string urlToQuery, out string? retrievedUrl );
        bool Save ( string shortenedUrl, string originalUrl );
    }
}
