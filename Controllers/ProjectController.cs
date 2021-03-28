using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace engine_plugin_backend.Controllers
{
    [ApiController]
    [Route("api/project")]
    public class ProjectController: Controller
    {
        [Authorize]
        [HttpGet]
        [Route("getProject")]
        public ActionResult<string> GetProject()
        {
            return Json(User.Identity.Name);
        }
    }
}