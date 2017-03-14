using System.Collections.Generic;
using System.Linq;
using indiedevlog.web.Common;
using indiedevlog.web.EFModel;
using indiedevlog.web.EFModel.Objects.SPs;
using indiedevlog.web.EFModel.Objects.Tables;
using indiedevlog.web.Models;
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

        public ReturnSet<ProjectPlanListingModel> GetProjectPlans(string projectName)
        {
            using (var eFactory = new EntityFactory(GlobalSettings.DatabaseConnection))
            {
                var result =
                    eFactory.Set<getLatestPlanUpdatesForProjectSP>()
                        .FromSql($"dbo.getLatestPlanUpdatesForProjectSP @ProjectName = '{DeserializeString(projectName)}', @RowCount = {9}")
                        .ToList()
                        .Select(a => new PlanUpdateResponseItem
                        {
                            Subject = a.Subject,
                            Body = a.Body,
                            AuthorName = a.DisplayName,
                            ProjectName = a.ProjectName,
                            PostDate = a.Created
                        }).ToList();

                var model = new ProjectPlanListingModel
                {
                    PlanUpdates = result,
                    ProjectName = DeserializeString(projectName)
                };

                return new ReturnSet<ProjectPlanListingModel>(model);
            }
        }

        public ReturnSet<UserPlanListingModel> GetUserPlans(string userDisplayName)
        {
            using (var eFactory = new EntityFactory(GlobalSettings.DatabaseConnection))
            {
                var result =
                    eFactory.Set<getLatestPlanUpdatesForUserSP>()
                        .FromSql($"dbo.getLatestPlanUpdatesForUsersSP @UserDisplayName = '{DeserializeString(userDisplayName)}', @RowCount = {9}")
                        .ToList()
                        .Select(a => new PlanUpdateResponseItem
                        {
                            Subject = a.Subject,
                            Body = a.Body,
                            AuthorName = a.DisplayName,
                            ProjectName = a.ProjectName,
                            PostDate = a.Created
                        }).ToList();

                var model = new UserPlanListingModel()
                {
                    PlanUpdates = result,
                    UserDisplayName = DeserializeString(userDisplayName)
                };

                return new ReturnSet<UserPlanListingModel>(model);
            }
        }
    }
}