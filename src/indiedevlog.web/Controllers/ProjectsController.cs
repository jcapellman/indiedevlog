using System.Linq;

using indiedevlog.web.EFModel;
using indiedevlog.web.EFModel.Objects.SPs;
using indiedevlog.web.Settings;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace indiedevlog.web.Controllers
{
    public class ProjectsController : BaseController
    {
        public ProjectsController(IOptions<GlobalSettings> globalSettings) : base(globalSettings.Value)
        {
        }

        public ActionResult CreateProject(string name)
        {
            return RedirectToAction("Index", "Projects");
        }

        public ActionResult Index()
        {
            using (var dbFactory = new EntityFactory(_globalSettings.DatabaseConnection))
            {
                return View(dbFactory.Set<getUserProjectsSP>().FromSql($"dbo.getUserProjectsSP @UserID = {UserID}").ToList());
            }
        }
    }
}