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

        // dependency injection is used to initialise ScaffoldService
        // See Startup.cs file to see how it was registered
        public ScaffoldController(ScaffoldService scaffoldService)
        {
            _scaffoldedService = scaffoldService;
        }

        [HttpGet("getScaffoldedData/{id?}")]
        public ActionResult<ScaffoldModel> GetScaffoldData(string id)
        {
            // TO-DO Search for scaffolded data by projectName(?)
            return new ScaffoldModel() { Name = "Name", Domain = "Domain", PositionType = "PositionType", SeniorityLevel = "SeniorityLevel", MainTechnology = "Main", AdditionalTechnologies = null };//_scaffoldedService.GetScaffoldedData(id);
        }

        // part of the data like PositionType and SeniorityLevel
        // is selected by manager, so it's get via body
        // other data is got from mock-company site by scrapping the site
        [HttpPost("addScaffoldedData")]
        public ActionResult<string> AddScaffoldedData([FromBody] ScaffoldModel scaffoldData)
        {
            return _scaffoldedService.AddScaffoldedData(scaffoldData);
        }
    }
}