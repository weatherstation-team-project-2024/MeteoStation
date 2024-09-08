using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using api.Data;
using api.Models;

namespace api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StationsController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly ILogger<StationsController> _logger;

        public StationsController(DataContext context, ILogger<StationsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetStations()
        {
            try
            {
                var updatedStations = await _context.Stations.ToListAsync();
                DateTimeOffset dateTimeOffset = DateTimeOffset.Now;
                long unixTimestamp = dateTimeOffset.ToUnixTimeSeconds();

                return Ok(new StationsResponse { Stations = updatedStations, GeneratedAt = unixTimestamp });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the request");
                return StatusCode(500, "An error occurred while processing your request");
            }
        }

        [HttpGet("{ids}")]
        public async Task<IActionResult> GetStationsByIds(string ids)
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

                var stations = await _context.Stations
                                              .Where(station => idList.Contains(station.StationId))
                                              .ToListAsync();

                if (stations.Count == 0)
                {
                    _logger.LogWarning("No stations found for IDs: {Ids}", ids);
                    return NotFound($"No stations found for IDs: {ids}");
                }

                DateTimeOffset dateTimeOffset = DateTimeOffset.Now;
                long unixTimestamp = dateTimeOffset.ToUnixTimeSeconds();

                return Ok(new StationsResponse { Stations = stations, GeneratedAt = unixTimestamp });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the request for IDs: {Ids}", ids);
                return StatusCode(500, "An error occurred while processing your request");
            }
        }
    }
}
