using CQRS.Commands;
using Microsoft.AspNetCore.Mvc;
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
        private readonly ShortenerService _service;

        public ShortenerController ( ILogger<ShortenerController> logger, ShortenerService service )
        {
            _logger = logger;
            _service = service;
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
                //TODO: Fetch URL from repository?
                // var lengthenedUrl = _repository.FetchShortenedUrl( new Get<string>( urlToFetch  ) );
                // _logger.LogDebug( "Received {lengthenedUrl} for {shortenedUrl}", lengthenedUrl, urlToFetch );
                //return Ok( lengthenedUrl );
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
        public IActionResult ShortenUrl ( [Required(AllowEmptyStrings = false)] string urlToShorten )
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