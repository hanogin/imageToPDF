//using API.Dal;
using Microsoft.AspNetCore.Mvc;
using Service;
using Service.DTO;
using ILogger = Serilog.ILogger;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstructorsController : ControllerBase
    {
        private InstructorService _instructorService;
        private readonly ILogger _logger;

        public InstructorsController(
            InstructorService instructorService,
            ILogger logger)
        {
            _instructorService = instructorService;
            _logger = logger;
        }

        [HttpGet]
        public List<InstructorRes> GetAll()
        {
            return _instructorService.GetAll();
        }

        [HttpPost]
        public void Post([FromBody] InstructorCreate value)
        {
            _instructorService.Create(value);
        }

        [HttpPut]
        public void Put([FromBody] InstructorUpdate value)
        {
            _instructorService.Update(value);
        }
    }
}
