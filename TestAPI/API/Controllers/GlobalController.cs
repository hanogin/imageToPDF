//using API.Dal;
using DAL.Context;
using Microsoft.AspNetCore.Mvc;
using Service;
using Service.DTO;
using ILogger = Serilog.ILogger;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GlobalController : ControllerBase
    {
        //private ClientService _clientService;
        private IDapperBaseRepository _dapper;
        private readonly ILogger _logger;
        //private SpRequest _sp_request;

        public GlobalController(
            //SpRequest spRequest,
            ClientService clientService,
            IDapperBaseRepository dapperBaseRepository,
            ILogger logger)
        {
            _dapper = dapperBaseRepository;
            //_sp_request = spRequest;
            //_clientService = clientService;
            _logger = logger;
        }

        // GET api/<ClientsController>
        [HttpGet("table/{tableName}")]
        public IActionResult GetTTable(string tableName)
        {

            return Ok(_dapper.Query<TCodeDTO>($"SELECT {tableName}Id AS Id, {tableName}Desc AS [Desc] FROM T_{tableName}"));

            //return Ok(_clientService.GetAll());
        }
    }
}
