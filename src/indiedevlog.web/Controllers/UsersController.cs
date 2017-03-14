using indiedevlog.web.Managers;
using indiedevlog.web.Settings;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace indiedevlog.web.Controllers
{
    public class UsersController : BaseController
    {
        public UsersController(IOptions<GlobalSettings> globalSettings) : base(globalSettings.Value)
        {
        }

        [Route("/users/{userDisplayName}")]
        public ActionResult Index(string userDisplayName)
        {
            var result = new PlanManager(_globalSettings).GetUserPlans(userDisplayName);

            if (result.HasError)
            {
                return RedirectToAction("Error", "Home");
            }

            return View(result.ObjectValue);
        }
    }
}