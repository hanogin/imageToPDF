using Microsoft.AspNetCore.Mvc;
using Service;
using Service.DTO;
using System.IO;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using ILogger = Serilog.ILogger;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private ClientService _clientService;
        private readonly ILogger _logger;
        //private SpRequest _sp_request;

        public ClientsController(
            //SpRequest spRequest,
            ClientService clientService,
            ILogger logger)
        {
            //_sp_request = spRequest;
            _clientService = clientService;
            _logger = logger;
        }

        // GET api/<ClientsController>

        [HttpGet]
        public List<ClientForSearchRes> Get()
        {
            return _clientService.GetAll();
        }

        [HttpGet("manuall/{lessonId}")]
        public List<ClientForAddManuall> Get(int lessonId)
        {
            return _clientService.GetClientForManuall(lessonId);
        }

        [HttpGet("{id}")]
        public List<ClientRes> GetById(int id)
        {
            return _clientService.GetById(id);
        }

        [HttpPost]
        public int Post([FromBody] ClientCreate value)
        {
            return _clientService.Create(value);
        }



        [HttpPut]
        public void Put([FromBody] ClientUpdate value)
        {
            _clientService.Update(value);
        }



        #region Client file

        [HttpGet("file/{clientFileId}")]
        public ActionResult<ClientFileRes> GetFile(int clientFileId)
        {
            var file = _clientService.GetFile(clientFileId);
            var fileName = HttpUtility.UrlEncode(file.FileName, Encoding.UTF8);

            Response.Headers.Add("x-file-name", fileName);
            Response.Headers.Add("Access-Control-Expose-Headers", "x-file-name");
            return File(file.File, file.FileType);
        }

        [HttpGet("{clientId}/file")]
        public ActionResult<List<ClientFileNamesRes>> GetFileNames(int clientId)
        {
            return Ok(_clientService.GetFileNamesByCLientId(clientId));
        }


        [HttpPost("file")]
        public void PostFile([FromBody] ClientFileCreate file)
        {
            _clientService.CreatFile(file);
        }

        [HttpDelete("file/{clientFileId}")]
        public void PostFile(int clientFileId)
        {
            _clientService.DeleteFile(clientFileId);
        }

        #endregion

    }
}
