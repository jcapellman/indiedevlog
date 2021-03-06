﻿using System;
using System.Linq;

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
        
        public int UserID
        {
            get
            {
                var claims = User.Claims.ToList();

                var userClaim = claims.FirstOrDefault(a => a.Type == "userid");

                return userClaim == null ? default(int) : Convert.ToInt32(userClaim.Value);
            }
        }
    }
}