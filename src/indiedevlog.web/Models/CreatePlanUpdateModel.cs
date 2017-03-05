using indiedevlog.web.EFModel.Objects.SPs;

using System.Collections.Generic;

namespace indiedevlog.web.Models
{
    public class CreatePlanUpdateModel
    {
        public string Subject { get; set; }

        public string Body { get; set; }

        public List<getUserProjectsSP> Projects { get; set; }

        public int SelectedProjectID { get; set; }
    }
}