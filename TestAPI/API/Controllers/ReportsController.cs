using Microsoft.AspNetCore.Mvc;
using Service;
using Service.DTO;
using System.IO;
using System.Net.Http.Headers;
using ILogger = Serilog.ILogger;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private ReportService _reportService;
        private readonly ILogger _logger;
        //private SpRequest _sp_request;

        public ReportsController(
            //SpRequest spRequest,
            ReportService reportService,
            ILogger logger)
        {
            //_sp_request = spRequest;
            _reportService = reportService;
            _logger = logger;
        }
        
        // GET api/<ClientsController>

        [HttpGet("maccabi")]
        public List<ReportMacabyRes> Get(DateTime date)
        {
            return _reportService.GetByDate(date);
        }
    }
}
