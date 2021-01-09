using Microsoft.AspNetCore.Mvc;
using engine_plugin_backend.Services;
using engine_plugin_backend.Models;

namespace engine_plugin_backend.Controllers
{
    [ApiController]
    [Route("api/scaffold")]
    public class ScaffoldController : ControllerBase
    {
        private readonly ScaffoldService _scaffoldedService;

        //dependency injection is used to initialise ScaffoldService
        // See Startup.cs file to see how it was registered
        public ScaffoldController(ScaffoldService scaffoldService)
        {
            _scaffoldedService = scaffoldService;
        }

        [HttpGet("getScaffoldedData/{id?}")]
        public ActionResult<ScaffoldModel> GetScaffoldData(string id)
        {
            // TO-DO Search for scaffolded data by projectName(?)
            return _scaffoldedService.GetScaffoldedData(id);
        }

        //data is passed via body of the request
        [HttpPost("addScaffoldedData")]
        public ActionResult<string> AddScaffoldedData([FromBody] ScaffoldModel scaffoldData)
        {
            return _scaffoldedService.AddScaffoldedData(scaffoldData);
        }
    }
}