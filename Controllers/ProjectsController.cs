using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using engine_plugin_backend.Services;
using engine_plugin_backend.Models;

namespace engine_plugin_backend.Controllers
{
    [ApiController]
    [Route("api/projects")]
    public class ProjectsController : Controller
    {
        private readonly ProjectsService _projectsService;

        public ProjectsController(ProjectsService projectsService)
        {
            _projectsService = projectsService;
        }

        [Authorize]
        [HttpGet]
        [Route("getAll")]
        public ActionResult<string> GetProjects()
        {
            if (User.Identity.IsAuthenticated)
            {
                var projects = _projectsService.GetAllProjects(User.Identity.Name);
                return Json(projects);
            }

            return BadRequest(new { error_text = "Unauthenticated access" });
        }

        [Authorize]
        [HttpPost]
        [Route("create")]
        public ActionResult<string> CreateProject([FromBody] ProjectModel project)
        {
            if (User.Identity.IsAuthenticated)
            {
                _projectsService.CreateProject(User.Identity.Name, project);
                return Ok();
            }

            return BadRequest(new { error_text = "Unauthenticated access" });
        }
    }
}