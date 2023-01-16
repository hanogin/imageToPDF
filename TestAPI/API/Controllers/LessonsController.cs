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
    public class LessonsController : ControllerBase
    {
        private LessonService _lessonService;
        private readonly ILogger _logger;

        public LessonsController(
            LessonService lessonService,
            ILogger logger)
        {
            _lessonService = lessonService;
            _logger = logger;
        }

        // GET api/<LessonsController>
        //[HttpPost]
        //public IActionResult GetAll()
        //{
        //    return Ok(new ResponseData(_sp_request.GetData("usp_lesson_select")).Data);
        //}

        //[HttpPost("upsert")]
        //public void Post([FromBody] DalParameter[] value)
        //{
        //    _sp_request.GetData("usp_lesson_upsert", value);
        //}

        //[HttpPost("clientInLesson")]
        //public IActionResult GetLessonClientIn([FromBody] DalParameter[] value)
        //{
        //    return Ok(new ResponseData(_sp_request.GetData("usp_clientInLesson_select_by_client", value)).Data);
        //}

        [HttpGet]
        public List<LessonRes> Get()
        {
            return _lessonService.GetAll();
        }

        [HttpGet("client/{clientId}")]
        public List<LessonRes> GetClientLesson(int clientId)
        {
            return _lessonService.GetClientLesson(clientId);
        }

        // POST api/<LessonsController>
        [HttpPost]
        public void Post([FromBody] LessonCreate value)
        {
            _lessonService.Create(value);
        }

        // PUT api/<LessonsController>
        [HttpPut]
        public void Put([FromBody] LessonUpdate value)
        {
            _lessonService.Update(value);
        }
    }
}
