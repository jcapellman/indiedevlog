using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using indiedevlog.web.Settings;
using Microsoft.AspNetCore.Mvc;

namespace indiedevlog.web.Controllers
{
    public class BaseController : Controller
    {
        protected GlobalSettings _globalSettings;
        
        public BaseController(GlobalSettings globalSettings)
        {
            _globalSettings = globalSettings;
        }
    }
}
