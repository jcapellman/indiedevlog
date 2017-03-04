using System.Collections.Generic;
using System.Linq;

using indiedevlog.web.EFModel;
using indiedevlog.web.EFModel.Objects.SPs;
using indiedevlog.web.Settings;

using Microsoft.EntityFrameworkCore;

namespace indiedevlog.web.Managers
{
    public class ProjectManager : BaseManager
    {
        public ProjectManager(GlobalSettings globalSettings) : base(globalSettings)
        {
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