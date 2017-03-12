using System.Collections.Generic;
using System.Linq;

using indiedevlog.web.EFModel;
using indiedevlog.web.EFModel.Objects.SPs;
using indiedevlog.web.EFModel.Objects.Tables;
using indiedevlog.web.Settings;

using Microsoft.EntityFrameworkCore;

namespace indiedevlog.web.Managers
{
    public class ProjectManager : BaseManager
    {
        public ProjectManager(GlobalSettings globalSettings) : base(globalSettings)
        {
        }

        public bool CreateProject(int userID, string projectName)
        {
            using (var dbFactory = new EntityFactory(GlobalSettings.DatabaseConnection))
            {
                var project = new Projects
                {
                    Name = projectName,
                    URLSafe = projectName.Replace(" ", "-")
                };

                dbFactory.Projects.Add(project);
                dbFactory.SaveChanges();

                var projectRelation = new Users2Projects
                {
                    UserID = userID,
                    ProjectID = project.ID
                };

                dbFactory.Users2Projects.Add(projectRelation);
                dbFactory.SaveChanges();

                return true;
            }
        }

        public List<getUserProjectsSP> GetUserProjects(int userID)
        {
            using (var dbFactory = new EntityFactory(GlobalSettings.DatabaseConnection))
            {
                return dbFactory.Set<getUserProjectsSP>().FromSql($"dbo.getUserProjectsSP @UserID = {userID}").ToList();
            }
        }
    }
}