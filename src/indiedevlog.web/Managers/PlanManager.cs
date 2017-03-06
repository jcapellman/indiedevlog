using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using indiedevlog.web.EFModel;
using indiedevlog.web.EFModel.Objects;
using indiedevlog.web.EFModel.Objects.Tables;
using indiedevlog.web.Settings;

namespace indiedevlog.web.Managers
{
    public class PlanManager : BaseManager
    {
        public PlanManager(GlobalSettings globalSettings) : base(globalSettings)
        {
        }

        public bool AddPlanUpdate(int userID, string subject, string body, int projectID)
        {
            using (var eFactory = new EntityFactory(GlobalSettings.DatabaseConnection))
            {
                var planUpdate = new PlanUpdates
                {
                    Body = body,
                    Subject = subject,
                    UserID = userID,
                    ProjectID = projectID
                };

                eFactory.PlanUpdates.Add(planUpdate);
                eFactory.SaveChanges();

                return true;
            }
        }
    }
}
