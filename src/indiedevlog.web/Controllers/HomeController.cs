using indiedevlog.web.Managers;
using indiedevlog.web.Settings;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace indiedevlog.web.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IOptions<GlobalSettings> globalSettings) : base(globalSettings.Value) { }
        
        [ResponseCache(CacheProfileName = "Default")]
        public IActionResult Index()
        {
            return View(new PlanManager(_globalSettings).GetLatestPlanUpdates());
        }

        public ActionResult Error(string exceptionStr) => View("Error", exceptionStr);
    }
}