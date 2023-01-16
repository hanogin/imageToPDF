using Microsoft.AspNetCore.Mvc;
using Service;
using Service.DTO;
using ILogger = Serilog.ILogger;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgramsController : ControllerBase
    {
        private ProgramService _programService;
        private readonly ILogger _logger;

        public ProgramsController(
           ProgramService programService,
            ILogger logger)
        {
            _logger = logger;
            _programService = programService;
        }

        //[HttpPost]
        //public IActionResult GetAll()
        //{
        //    return Ok(new ResponseData(_sp_request.GetData("usp_program_select")).Data);
        //}

        //[HttpPost("upsert")]
        //public void Post([FromBody] DalParameter[] value)
        //{
        //    _sp_request.GetData("usp_program_upsert", value);
        //}

        // GET api/<ProgramsController>
        [HttpGet]
        public List<ProgramPrRes> Get()
        {
            return _programService.GetAll();
        }

        // POST api/<ProgramsController>
        [HttpPost]
        public void Post([FromBody] ProgramPrCreate value)
        {
            _programService.Create(value);
        }

        // PUT api/<ProgramsController>
        [HttpPut]
        public void Put([FromBody] ProgramPrUpdate value)
        {
            _programService.Update(value);
        }
    }
}
