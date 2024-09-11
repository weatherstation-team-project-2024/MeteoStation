using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using api.Data;

namespace api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly ILogger<WeatherController> _logger;

        public WeatherController(DataContext context, ILogger<WeatherController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetCurrentWeather()
        {
            try
            {
                var currentWeather = await _context.Weather.ToListAsync();

                if (currentWeather.Count == 0)
                {
                    _logger.LogWarning("No current weather data found.");
                    return NotFound("No current weather data found.");
                }

                DateTimeOffset dateTimeOffset = DateTimeOffset.Now;
                long unixTimestamp = dateTimeOffset.ToUnixTimeSeconds();

                return Ok(new { Weather = currentWeather, GeneratedAt = unixTimestamp });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the request");
                return StatusCode(500, "An error occurred while processing your request");
            }
        }
    }
}

