using System.Collections.Generic;

using indiedevlog.web.EFModel.Objects.SPs;

namespace indiedevlog.web.Models
{
    public class ProjectModel
    {
        public string Name { get; set; }

        public List<getUserProjectsSP> ProjecListing { get; set; }
    }
}