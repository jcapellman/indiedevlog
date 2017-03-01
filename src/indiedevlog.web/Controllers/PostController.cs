using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using indiedevlog.web.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace indiedevlog.web.Controllers
{
    public class PostController : BaseController
    {
        public PostController(IOptions<GlobalSettings> globalSettings) : base(globalSettings.Value)
        {
        }

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
    }
}
