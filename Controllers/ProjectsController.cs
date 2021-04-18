using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using engine_plugin_backend.Services;
using engine_plugin_backend.Models;
using System.Collections.Generic;

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
        [HttpGet]
        [Route("get")]
        public ActionResult<string> GetProject([FromQuery]string id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var project = _projectsService.GetProject(User.Identity.Name, id);
                return Json(project);
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
                var id = _projectsService.CreateProject(User.Identity.Name, project);
                return Ok(id);
            }

            return BadRequest(new { error_text = "Unauthenticated access" });
        }

        [Authorize]
        [HttpPut]
        [Route("updateTech")]
        public ActionResult<string> UpdateTech([FromBody]string projectTech, [FromQuery] string projectId)
        {
            if (User.Identity.IsAuthenticated)
            {
                _projectsService.UpdateTech(projectTech, projectId);
                return Ok();
            }

            return BadRequest(new { error_text = "Unauthenticated access" });
        }
    }
}