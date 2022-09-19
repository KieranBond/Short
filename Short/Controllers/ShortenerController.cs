using CQRS.Commands;
using Microsoft.AspNetCore.Mvc;
using Short.Services;
using System.ComponentModel.DataAnnotations;

namespace Short.Controllers
{
    [ApiController]
    [Route( "[controller]" )]
    public class ShortenerController : ControllerBase
    {
        private readonly ILogger<ShortenerController> _logger;
        private readonly ShortenerService _service;

        public ShortenerController ( ILogger<ShortenerController> logger, ShortenerService service )
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        public string Get( [Required] string urlToShorten )
        {
            _logger.LogDebug( "Received GET for {url}", urlToShorten );

            var shortenedUrl = _service.ShortenUrl( new Handle<string>( urlToShorten ) );

            _logger.LogDebug( "Shortened {url} to {shortenedUrl}", urlToShorten, shortenedUrl );

            return shortenedUrl;
        }
    }
}