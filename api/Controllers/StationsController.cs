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

        public StationsController(DataContext context, ILogger<StationsController> logger