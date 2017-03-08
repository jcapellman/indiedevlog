using indiedevlog.web.Managers;
using indiedevlog.web.Models;
using indiedevlog.web.Settings;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace indiedevlog.web.Controllers
{
    public class ProjectsController : BaseController
    {
        public ProjectsController(IOptions<GlobalSettings> globalSettings) : base(globalSettings.Value)
        {
        }

        public ActionResult CreateProject(ProjectModel model)
        {
            var result = new ProjectManager(_globalSettings).CreateProject(UserID, model.Name);

            return RedirectToAction("Index", "Projects");
        }

        public ActionResult Index()
        {
            var model = new ProjectModel
            {
                Name = string.Empty,
                ProjecListing = new ProjectManager(_globalSettings).GetUserProjects(UserID)
            };

            return View(model);
        }
    }
}