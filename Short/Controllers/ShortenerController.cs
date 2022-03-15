using Microsoft.AspNetCore.Mvc;
using Short.Services;

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
        public bool Get()
        {
            _logger.LogDebug( "Received GET" );

            //_service.

            return false;
        }
    }
}