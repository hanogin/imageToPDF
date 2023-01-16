using Microsoft.AspNetCore.Mvc;
using Service;
using Service.DTO;
using ILogger = Serilog.ILogger;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitsController : ControllerBase
    {
        private VisitService _visitService;
        private readonly ILogger _logger;

        public VisitsController(
            VisitService visitService,
            ILogger logger)
        {
            _visitService = visitService;
            _logger = logger;
        }

        // GET api/<VisitsController>
        [HttpGet]
        public List<VisitRes> Get()
        {
            return _visitService.GetAll();
        }

        [HttpPost("report")]
        public List<VisitRes> GetReport(VisitReportReq value)
        {
            return _visitService.GetForReport(value);
        }

        [HttpGet("filter")]
        public List<VisitRes> GetByFilter([FromQuery] GetWithFilter value)
        {
            return _visitService.GetByFilter(value);
        }

        // POST api/<VisitsController>
        [HttpPost]
        public bool Post([FromBody] VisitCreate value)
        {
            return _visitService.Create(value);
        }

        // PUT api/<VisitsController>
        [HttpPut]
        public void Put([FromBody] VisitUpdate value)
        {
            _visitService.Update(value);
        }

        // DELETE api/<VisitsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _visitService.Delete(id);
        }


        [HttpPost("groupPresence")]
        public void Post([FromBody] VisitGroupPresenceCreate value)
        {
            _visitService.CreateGroupPresence(value);
        }
    }
}
