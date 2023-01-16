using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.DTO;
using Service.Interface;
using ILogger = Serilog.ILogger;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        private readonly ILogger _logger;

        public UsersController(
            IUserService userService,
            ILogger logger)
        {
            _userService = userService;
            _logger = logger;
        }


        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            //_logger.Debug("ss");
            AuthenticateResponse? response = _userService.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        //[Authorize]
        //[HttpGet]
        //public IActionResult GetAll()
        //{
        //    var users = _userService.GetAll();
        //    return Ok(users);
        //}

        //[Authorize]
        //[HttpPost]
        //public void Post([FromBody] UserCreateDTO value)
        //{
        //    _userService.Create(value);
        //}

        //[Authorize]
        //[HttpPut]
        //public void Put([FromBody] UserUpdateDTO value)
        //{
        //    _userService.Update(value);
        //}


        //[Authorize]
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //    _userService.Delete(id);
        //}


        //[Authorize]
        //[HttpGet("role")]
        //public ActionResult<List<TCodeDTO>> GetRole()
        //{
        //    return Ok(_userService.GetUserRole());
        //}
    }
}
