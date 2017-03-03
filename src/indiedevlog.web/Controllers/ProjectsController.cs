﻿using System.Linq;

using indiedevlog.web.EFModel;
using indiedevlog.web.EFModel.Objects.SPs;
using indiedevlog.web.EFModel.Objects.Tables;
using indiedevlog.web.Models;
using indiedevlog.web.Settings;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace indiedevlog.web.Controllers
{
    public class ProjectsController : BaseController
    {
        public ProjectsController(IOptions<GlobalSettings> globalSettings) : base(globalSettings.Value)
        {
        }

        public ActionResult CreateProject(ProjectModel model)
        {
            using (var dbFactory = new EntityFactory(_globalSettings.DatabaseConnection))
            {
                var project = new Projects
                {
                    Name = model.Name
                };

                dbFactory.Projects.Add(project);
                dbFactory.SaveChanges();

                var projectRelation = new Users2Projects
                {
                    UserID = UserID,
                    ProjectID = project.ID
                };

                dbFactory.Users2Projects.Add(projectRelation);
                dbFactory.SaveChanges();

                return RedirectToAction("Index", "Projects");
            }
        }

        public ActionResult Index()
        {
            using (var dbFactory = new EntityFactory(_globalSettings.DatabaseConnection))
            {
                return View(dbFactory.Set<getUserProjectsSP>().FromSql($"dbo.getUserProjectsSP @UserID = {UserID}").ToList());
            }
        }
    }
}