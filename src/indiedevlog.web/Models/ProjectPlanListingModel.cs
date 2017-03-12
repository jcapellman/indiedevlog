using System.Collections.Generic;

using indiedevlog.web.Objects.PlanUpdates;

namespace indiedevlog.web.Models
{
    public class ProjectPlanListingModel
    {
        public string ProjectName { get; set; }

        public List<PlanUpdateResponseItem> PlanUpdates { get; set; }
    }
}