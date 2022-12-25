using CQRS.Commands;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Short.Repositories;
using Short.Services;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Short.Controllers
{
    [ApiController]
    [Route( "[controller]" )]
    public class ShortenerController : Controller
    {
        private readonly ILogger<ShortenerController> _logger;
        private readonly IShortenerService _service;
        private readonly IShortenerRepository _repository;

        public ShortenerController ( ILogger<ShortenerController> logger, IShortenerService service, IShortenerRepository repository )
        {
            _logger = logger;
            _service = service;
            _repository = repository;
        }

        [HttpGet]
        [Route("/fetch")]
        [SwaggerResponse( (int)HttpStatusCode.OK )]
        [SwaggerResponse( (int)HttpStatusCode.NotFound )]
        public IActionResult FetchShortenedUrl( [Required(AllowEmptyStrings = false)] string urlToFetch )
        {
            _logger.LogDebug( "Received GET for {shortenedUrl}", urlToFetch );

            try
            {
                _repository.TryRetrieve( urlToFetch, out var lengthenedUrl );
                _logger.LogDebug( "Received {lengthenedUrl} for {shortenedUrl}", lengthenedUrl, urlToFetch );
                return Ok( lengthenedUrl );
            }
            catch ( Exception ex )
            {
                _logger.LogError( ex, "Error thrown when fetching lengthened URL for {shortenedUrl}", urlToFetch );
            }

            return NotFound( urlToFetch );
        }

        [HttpGet]
        [Route("/shorten")]
		[SwaggerResponse( (int)HttpStatusCode.OK )]
        [SwaggerResponse( (int)HttpStatusCode.InternalServerError )]
        public IActionResult ShortenUrl ( [Required] string urlToShorten )
        {
            _logger.LogDebug( "Received GET to shorten {url}", urlToShorten );

            try
            {
                var shortenedUrl = _service.ShortenUrl( new Handle<string>( urlToShorten ) );
                _logger.LogDebug( "Shortened {url} to {shortenedUrl}", urlToShorten, shortenedUrl );
                return Ok( shortenedUrl );
            }
            catch ( Exception ex )
            {
                _logger.LogError( ex, "Failed to shortern {url}", urlToShorten );
                return StatusCode( 500 );
            }
        }
    }
}