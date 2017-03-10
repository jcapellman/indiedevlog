using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace indiedevlog.web.Models
{
    public class PlanUpdateModel
    {
        public string Subject { get; set; }

        public string Body { get; set; }

        public string Author { get; set; }

        public string ProjectName { get; set; }

        public DateTime TimeStamp { get; set; }
    }
}
