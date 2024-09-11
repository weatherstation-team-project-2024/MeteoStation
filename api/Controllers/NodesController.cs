using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using api.Data;

using api.Models.ResponseModels;

namespace api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NodesController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly ILogger<NodesController> _logger;

        public NodesController(DataContext context, ILogger<NodesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetNodes()
        {
            try
            {
                var updatedNodes = await _context.Nodes.ToListAsync();
                DateTimeOffset dateTimeOffset = DateTimeOffset.Now;
                long unixTimestamp = dateTimeOffset.ToUnixTimeSeconds();

                return Ok(new NodeResponse { Nodes = updatedNodes, GeneratedAt = unixTimestamp });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the request");
                return StatusCode(500, "An error occurred while processing your request");
            }
        }

        [HttpGet("{ids}")]
        public async Task<IActionResult> GetNodesByIds(string ids)
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

                var nodes = await _context.Nodes
                                          .Where(node => idList.Contains(node.NodeId))
                                          .ToListAsync();

                if (nodes.Count == 0)
                {
                    _logger.LogWarning("No nodes found for IDs: {Ids}", ids);
                    return NotFound($"No nodes found for IDs: {ids}");
                }

                DateTimeOffset dateTimeOffset = DateTimeOffset.Now;
                long unixTimestamp = dateTimeOffset.ToUnixTimeSeconds();

                return Ok(new NodeResponse { Nodes = nodes, GeneratedAt = unixTimestamp });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the request for IDs: {Ids}", ids);
                return StatusCode(500, "An error occurred while processing your request");
            }
        }
    }
}