using System;

namespace indiedevlog.web.Objects.PlanUpdates
{
    public class PlanUpdateResponseItem
    {
        public string ProjectName { get; set; }

        public string Subject { get; set; }

        private string _body;

        public string Body
        {
            get { return _body; }

            set { _body = value.Replace(System.Environment.NewLine, "<br/>"); }
        }

        public string AuthorName { get; set; }

        public DateTime PostDate { get; set; }

        public string ProjectNameURLSafe { get; set; }
    }
}