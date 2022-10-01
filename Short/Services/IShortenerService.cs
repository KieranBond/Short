using CQRS.Commands;

namespace Short.Services
{
    public interface IShortenerService
    {
        string ShortenUrl ( Handle<string> cmd );
    }
}
