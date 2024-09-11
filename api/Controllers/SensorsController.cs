using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using api.Data;
using api.Models;
using api.Models.ResponseModels;

namespace api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SensorsController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly ILogger<SensorsController> _logger;

        public SensorsController(DataContext context, ILogger<SensorsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetSensors()
        {
            try
            {
                var updatedSensors = await _context.Sensors.ToListAsync();
                DateTimeOffset dateTimeOffset = DateTimeOffset.Now;
                long unixTimestamp = dateTimeOffset.ToUnixTimeSeconds();

                return Ok(new SensorsResponse { Sensors = updatedSensors, GeneratedAt = unixTimestamp });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the request");
                return StatusCode(500, "An error occurred while processing your request");
            }
        }

        [HttpGet("{ids}")]
        public async Task<IActionResult> GetSensorsByIds(string ids)
        {
            try
            {
                #pragma warning disable CS8629 // Nullable value type may be null.
                var idList = ids.Split(',')
                                .Select(id =>
                                {
                                    return int.TryParse(id, out var parsedId) ? parsedId : (int?)null;
                                })
                                .Where(id => id.HasValue)
                                .Select(id => id.Value)
                                .ToList();
                #pragma warning restore CS8629 // Nullable value type may be null.

                if (idList.Count == 0)
                {
                    _logger.LogWarning("No valid IDs provided.");
                    return BadRequest("No valid IDs provided.");
                }

                var sensors = await _context.Sensors
                                              .Where(sensor => idList.Contains(sensor.Lsid))
                                              .ToListAsync();

                if (sensors.Count == 0)
                {
                    _logger.LogWarning("No sensors found for IDs: {Ids}", ids);
                    return NotFound($"No sensors found for IDs: {ids}");
                }

                DateTimeOffset dateTimeOffset = DateTimeOffset.Now;
                long unixTimestamp = dateTimeOffset.ToUnixTimeSeconds();

                return Ok(new SensorsResponse { Sensors = sensors, GeneratedAt = unixTimestamp });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the request for IDs: {Ids}", ids);
                return StatusCode(500, "An error occurred while processing your request");
            }
        }
    }
}
