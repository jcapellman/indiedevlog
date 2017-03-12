using System.Collections.Generic;

using indiedevlog.web.Objects.PlanUpdates;

namespace indiedevlog.web.Models
{
    public class UserPlanListingModel
    {
        public string UserDisplayName { get; set; }

        public List<PlanUpdateResponseItem> PlanUpdates { get; set; }
    }
}