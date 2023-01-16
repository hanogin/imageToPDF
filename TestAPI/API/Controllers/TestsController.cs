using Microsoft.AspNetCore.Mvc;
using Service;
using Service.DTO;
using ILogger = Serilog.ILogger;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestsController : ControllerBase
    {
        private TestService _testService;
        private readonly ILogger _logger;

        public TestsController(
            TestService testService,
            ILogger logger)
        {
            _testService = testService;
            _logger = logger;
        }


        [HttpGet("isAlive")]
        public IActionResult GetByFilter()
        {
            _testService.KeepServerALive();
            return Ok();
        }
    }
}
