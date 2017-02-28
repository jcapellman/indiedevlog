using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace indiedevlog.web.Settings
{
    public class GlobalSettings
    {
        public string SiteName { get; set; }

        public int NumPostsToList { get; set; }

        public string DatabaseConnection { get; set; }
    }
}