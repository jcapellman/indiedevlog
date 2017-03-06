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

        [Authorize]
        public ActionResult CreatePlanUpdate(CreatePlanUpdateModel model)
        {
            var planResult = new PlanManager(_globalSettings).AddPlanUpdate(UserID, model.Subject, model.Body,
                model.SelectedProjectID);
            
            return planResult ? RedirectToAction("Index", "Home") : RedirectToAction("Index", "Home");
        }
    }
}