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
    public class ClientsInLessonController : ControllerBase
    {
        private ClientInLesonService _clientInLesonService;
        private readonly ILogger _logger;

        public ClientsInLessonController(
            ClientInLesonService clientInLesonService,
            ILogger logger)
        {
            _clientInLesonService = clientInLesonService;
            _logger = logger;
        }

        [HttpGet("lesson/{lessonId}/{date}")]
        public List<ClientInLessonRes> GetClientInLesson(int lessonId, DateTime date)
        {
            return _clientInLesonService.GetClientInLesson(lessonId, date);
        }


        [HttpGet("notInlesson/{lessonId}")]
        public List<ClientInLessonRes> GetActiveClientNotInLesson(int lessonId)
        {
            return _clientInLesonService.GetClientNotInLesson(lessonId);
        }

        // POST api/<ClientsInLessonController>
        [HttpPost]
        public void Post([FromBody] ClientInLessonCreate value)
        {
            _clientInLesonService.InsertClient(value);
        }

        [HttpPost("list")]
        public void PostClientInLesson([FromBody] ClientInLessonsCreate value)
        {
            _clientInLesonService.UpdateClientInLesson(value);
        }

        // DELETE api/<ClientsInLessonController>/5
        [HttpDelete("client/{clientId}/lesson/{lessonId}")]
        public void Delete(int clientId, int lessonId)
        {
            _clientInLesonService.RemoveClientFromLesson(clientId, lessonId);
        }
    }
}
