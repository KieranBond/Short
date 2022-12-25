using CQRS.Commands;
using Short.Repositories;

namespace Short.Services
{
    public sealed class ShortenerService : IShortenerService
    {
        private readonly IShortenerRepository _repository; 

        public ShortenerService( IShortenerRepository repository )
        {
            _repository = repository;
        }

        public string ShortenUrl( Handle<string> cmd )
        {
            var dto = cmd.Dto;
            
            if ( ! _repository.TryRetrieve( dto, out var result ) && result != null ) 
            {
                return result;
            }

            // TODO: We can't guarantee currently that this value is unique...
            var shortenedUrl = Guid.NewGuid().ToString()[ ..5 ];
            _repository.Save( dto, shortenedUrl );
            
            return shortenedUrl;
        }
    }
}
