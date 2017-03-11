using indiedevlog.web.Managers;
using indiedevlog.web.Models;
using indiedevlog.web.Settings;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace indiedevlog.web.Controllers
{
    public class PlanUpdatesController : BaseController
    {
        public PlanUpdatesController(IOptions<GlobalSettings> globalSettings) : base(globalSettings.Value)
        {   
        }
        
        [Authorize]
        public ActionResult Index()
        {
            var model = new CreatePlanUpdateModel
            {
                Projects = new ProjectManager(_globalSettings).GetUserProjects(UserID)
            };

            return View(model);
        }

        [Route("/Project/{projectName}")]
        public ActionResult Index(string projectName)
        {
            var model = new PlanManager(_globalSettings).GetProjectPlans(projectName);

            return View("ProjectPlanListing", model);
        }

        [Authorize]
        public ActionResult CreatePlanUpdate(CreatePlanUpdateModel model)
        {
            var planResult = new PlanManager(_globalSettings).AddPlanUpdate(UserID, model.Subject, model.Body,
                model.SelectedProjectID);

            if (planResult)
            {
                return RedirectToAction("Index", "Home");
            }

            return View("Index", model);
        }
    }
}