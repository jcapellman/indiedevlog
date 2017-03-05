using System;
using System.ComponentModel.DataAnnotations;

using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace indiedevlog.web.EFModel.Objects.SPs
{
    public class getLatestPlanUpdatesSP
    {
        [Key]
        public int ID { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public DateTime Created { get; set; }

        public string DisplayName { get; set; }

        public string ProjectName { get; set; }
    }
}