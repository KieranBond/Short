using Microsoft.AspNetCore.Mvc;

namespace Short.Controllers
{
    [ApiController]
    [Route( "[controller]" )]
    public class Shortener : ControllerBase
    {
        private readonly ILogger<Shortener> _logger;

        public Shortener ( ILogger<Shortener> logger )
        {
            _logger = logger;
        }

        [HttpGet]
        public bool Get()
        {
            return false;
        }
    }
}