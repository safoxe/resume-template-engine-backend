using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using engine_plugin_backend.Services;
using engine_plugin_backend.Models;

namespace engine_plugin_backend.Controllers
{
    [ApiController]
    [Route("api/resumes")]

    public class ResumeController: Controller
    {
        private readonly ResumeService _resumeService;

        public ResumeController(ResumeService resumeService)
        {
            _resumeService = resumeService;
        }

        [Authorize]
        [HttpGet]
        [Route("get")]
        public ActionResult<string> GetResume([FromQuery]string id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var resumes = _resumeService.GetResumes(User.Identity.Name, id);
                return Json(resumes);
            }

            return BadRequest(new { error_text = "Unauthenticated access" });
        }

        [Authorize]
        [HttpPost]
        [Route("create")]
        public ActionResult<string> CreateProject([FromBody] ResumeModel resume)
        {
            if (User.Identity.IsAuthenticated)
            {
                _resumeService.CreateResume(resume);
                return Ok();
            }

            return BadRequest(new { error_text = "Unauthenticated access" });
        }
    }
}