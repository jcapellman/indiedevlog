using indiedevlog.web.Managers;
using indiedevlog.web.Settings;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace indiedevlog.web.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IOptions<GlobalSettings> globalSettings) : base(globalSettings.Value) { }
        
        [ResponseCache(NoStore = false, Duration = 3600)]
        public IActionResult Index()
        {
            return View(new PlanManager(_globalSettings).GetLatestPlanUpdates(_globalSettings.NumPostsToList));
        }
    }
}