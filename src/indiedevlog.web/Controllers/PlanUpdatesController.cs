using indiedevlog.web.EFModel;
using indiedevlog.web.EFModel.Objects;
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
            using (var eFactory = new EntityFactory(_globalSettings.DatabaseConnection))
            {
                var planUpdate = new PlanUpdates
                {
                    Body = model.Body,
                    Subject = model.Subject,
                    UserID = UserID
                };

                eFactory.PlanUpdates.Add(planUpdate);
                eFactory.SaveChanges();

                return RedirectToAction("Index", "Home");
            }
        }
    }
}