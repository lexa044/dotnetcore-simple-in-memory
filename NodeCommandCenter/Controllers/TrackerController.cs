using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NodeCommandCenter.Caching;

namespace NodeCommandCenter.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TrackerController : ControllerBase
    {
        private readonly Cache<string, Player> _cache;
        private readonly ILogger<WeatherForecastController> _logger;

        public TrackerController(ILogger<WeatherForecastController> logger, Cache<string, Player> cache)
        {
            _logger = logger;
            _cache = cache;
        }

        [HttpGet]
        public IEnumerable<Player> Get()
        {
            return _cache.GetAllValues();
        }

        [HttpPost]
        public IActionResult Register([FromBody] Player request)
        {
            request.IpAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            _cache.Set(request.Id, request);

            return Ok();
        }
    }
}
