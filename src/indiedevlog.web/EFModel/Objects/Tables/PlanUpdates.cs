namespace indiedevlog.web.EFModel.Objects.Tables
{
    public class PlanUpdates : BaseTable
    {
        public int UserID { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public int ProjectID { get; set; }
    }
}