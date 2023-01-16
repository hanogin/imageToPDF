using Microsoft.AspNetCore.Mvc;
using Service;
using Service.DTO;
using ILogger = Serilog.ILogger;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private AccountService _accountService;
        private readonly ILogger _logger;

        public AccountsController(
            AccountService accountService,
            ILogger logger)
        {
            _accountService = accountService;
            _logger = logger;
        }


        [HttpGet()]
        public List<AccountRes> GetByFilter([FromQuery] GetWithFilter value)
        {
            return _accountService.GetByFilter(value);
        }

        // POST api/<VisitsController>
        [HttpPost]
        public void Post([FromBody] AccountAddDeposit value)
        {
            _accountService.AddDeposit(value);
        }

        // DELETE api/<VisitsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _accountService.Delete(id);
        }
    }
}
