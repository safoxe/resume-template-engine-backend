using Microsoft.AspNetCore.Mvc;
using engine_plugin_backend.Services;
using engine_plugin_backend.Models;

namespace engine_plugin_backend.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class SignUpController : ControllerBase
    {
        private readonly SignUpService _signUpService;

        // dependency injection is used to initialise SignUpService
        // See Startup.cs file to see how it was registered
        public SignUpController(SignUpService signUpService)
        {
            _signUpService =  signUpService;
        }

        // part of the data like PositionType and SeniorityLevel
        // is selected by manager, so it's get via body
        // other data is got from mock-company site by scrapping the site
        [HttpPost("signUpUser")]
        public ActionResult<string> SignUpUser([FromBody] UserModel userModel)
        {
            return _signUpService.SignUpUser(userModel);
        }
    }
}