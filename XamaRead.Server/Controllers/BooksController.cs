using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace XamaRead.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
 
        private readonly ILogger<BooksController> _logger;

        public BooksController(ILogger<BooksController> logger)
        {
            _logger = logger;
        }

        [HttpGet("All")]
        public IEnumerable<string> Get()
        {
            return new List<string> { "Harry Potter", "Inception" };
        }

        [HttpPost]
        public IActionResult Post()
        {
            return Ok();
        }
    }
}
