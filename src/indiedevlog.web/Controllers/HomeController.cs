using System.Linq;

using indiedevlog.web.EFModel;
using indiedevlog.web.EFModel.Objects.SPs;
using indiedevlog.web.Settings;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
                return View(dbFactory.Set<getLatestPlanUpdatesSP>().FromSql($"dbo.getLatestPlanUpdatesSP @RowCount = {_globalSettings.NumPostsToList}").ToList());
            }
        }
    }
}