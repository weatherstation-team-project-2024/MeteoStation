using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

using api.Data;

namespace api.Controllers
{
    [Route("api/[controller]")]
    public class NodesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<NodesController> _logger;

        public NodesController(ApplicationDbContext context, ILogger<NodesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetNodes()
        {
            try
            {
                Console.WriteLine("Fetching Nodes logic.");
                
                return await Task.FromResult(Ok("/api/nodes"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the request");

                return await Task.FromResult(StatusCode(500, "An error occurred while processing your request"));
            }
        }
    }
}
