using Microsoft.AspNetCore.Mvc;

namespace engine_plugin_backend.Controllers
{
    [ApiController]
    [Route("api/scaffold")]
    public class ScaffoldController : ControllerBase
    {
        [HttpGet("getScaffoldedData")]
        public ActionResult<string> GetScaffoldData()
        {
            return "result";
        }
        [HttpPost("setScaffoldedData")]
        public ActionResult<string> SetScaffoldedData()
        {
            return "result";
        }
    }
}