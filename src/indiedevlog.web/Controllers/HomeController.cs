using System.Linq;
using indiedevlog.web.EFModel;
using indiedevlog.web.Settings;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace indiedevlog.web.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IOptions<GlobalSettings> globalSettings) : base(globalSettings.Value) { }

        public IActionResult Index()
        {
            using (var dbFactory = new EntityFactory(_globalSettings.DatabaseConnection))
            {
                return View(dbFactory.PlanUpdates.Where(a => a.Active).ToList());
            }
        }
    }
}