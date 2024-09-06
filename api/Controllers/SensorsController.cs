using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using api.Data;

namespace api.Controllers
{
    [Route("api/[controller]")]
    public class SensorsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<SensorsController> _logger;

        public SensorsController(ApplicationDbContext context, ILogger<SensorsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetSensors()
        {
            try
            {
                Console.WriteLine("Fetching Sensors logic.");
                
                return await Task.FromResult(Ok("/api/sensors"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the request");
                return await Task.FromResult(StatusCode(500, "An error occurred while processing your request"));
            }
        }

        [HttpGet("{sensorIds}")]
        public async Task<IActionResult> GetSensorsByIds(string sensorIds)
        {
            try
            {
                Console.WriteLine($"Fetching Sensors for IDs: {sensorIds}");
                
                return await Task.FromResult(Ok($"/api/sensors/{sensorIds}"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the request for IDs: {SensorIds}", sensorIds);
                return await Task.FromResult(StatusCode(500, "An error occurred while processing your request"));
            }
        }

        [HttpGet("sensor-activity")]
        public async Task<IActionResult> GetSensorActivity()
        {
            try
            {
                Console.WriteLine("Fetching Sensor Activity logic.");
                
                return await Task.FromResult(Ok("/api/sensor-activity"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the request");
                return await Task.FromResult(StatusCode(500, "An error occurred while processing your request"));
            }
        }

        [HttpGet("sensor-activity/{sensorIds}")]
        public async Task<IActionResult> GetSensorActivityByIds(string sensorIds)
        {
            try
            {
                Console.WriteLine($"Fetching Sensor Activity for IDs: {sensorIds}");
                
                return await Task.FromResult(Ok($"/api/sensor-activity/{sensorIds}"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the request for IDs: {SensorIds}", sensorIds);
                return await Task.FromResult(StatusCode(500, "An error occurred while processing your request"));
            }
        }

        [HttpGet("sensor-catalog")]
        public async Task<IActionResult> GetSensorCatalog()
        {
            try
            {
                Console.WriteLine("Fetching Sensor Catalog logic.");
                
                return await Task.FromResult(Ok("/api/sensor-catalog"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the request");
                return await Task.FromResult(StatusCode(500, "An error occurred while processing your request"));
            }
        }
    }
}
