using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace indiedevlog.web.EFModel.Objects
{
    public class PlanUpdates : BaseTable
    {
        public int UserID { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }
    }
}