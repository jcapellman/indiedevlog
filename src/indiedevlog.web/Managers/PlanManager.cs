using System.Collections.Generic;
using System.Linq;

using indiedevlog.web.EFModel;
using indiedevlog.web.EFModel.Objects.SPs;
using indiedevlog.web.EFModel.Objects.Tables;
using indiedevlog.web.Objects.PlanUpdates;
using indiedevlog.web.Settings;

using Microsoft.EntityFrameworkCore;

namespace indiedevlog.web.Managers
{
    public class PlanManager : BaseManager
    {
        public PlanManager(GlobalSettings globalSettings) : base(globalSettings)
        {
        }

        public List<PlanUpdateResponseItem> GetLatestPlanUpdates(int count)
        {
            using (var dbFactory = new EntityFactory(GlobalSettings.DatabaseConnection))
            {
                return
                    dbFactory.Set<getLatestPlanUpdatesSP>()
                        .FromSql($"dbo.getLatestPlanUpdatesSP @RowCount = {count}")
                        .ToList()
                        .Select(a => new PlanUpdateResponseItem
                        {
                            Body = a.Body,
                            Subject = a.Subject,
                            AuthorName = a.DisplayName,
                            PostDate = a.Created,
                            ProjectName = a.ProjectName
                        }).ToList();
            }
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