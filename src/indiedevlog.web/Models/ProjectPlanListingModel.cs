using System.Collections.Generic;

namespace indiedevlog.web.Models
{
    public class ProjectPlanListingModel
    {
        public string ProjectName { get; set; }

        public List<PlanUpdateModel> PlanUpdates { get; set; }
    }
}