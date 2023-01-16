using Microsoft.AspNetCore.Mvc;
using Service;
using Service.DTO;
using ILogger = Serilog.ILogger;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionsController : ControllerBase
    {
        private SubsService _subsService;
        //private SpRequest _sp_request;
        private readonly ILogger _logger;

        public SubscriptionsController(
            //SpRequest spRequest,
            SubsService subsService,
            ILogger logger)
        {
            //_sp_request = spRequest;
            _subsService = subsService;
            _logger = logger;
        }

        [HttpGet]
        public List<SubscriptionRes> GetByFilter([FromQuery] GetWithFilter model)
        {
            return _subsService.GetAll(model);
        }

        [HttpGet("active/client/{clientId}")]
        public List<SubscriptionActiveByClientRes> GetActiveSubsByClient(int clientId)
        {
            return _subsService.ActiveSubsByClientId(clientId);
        }

        [HttpGet("{subId}")]
        public SubscriptionRes GetByFilter(int subId)
        {
            return _subsService.GetBySubId(subId);
        }

        [HttpPost]
        public void Post([FromBody] SubscriptionCreate value)
        {
            _subsService.Create(value);
        }

        [HttpPut]
        public void Put([FromBody] SubscriptionUpdate value)
        {
            _subsService.Update(value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _subsService.DeleteSubs(id);
        }
    }
}
