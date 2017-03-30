namespace indiedevlog.web.Settings
{
    public class GlobalSettings
    {
        public string SiteName { get; set; }

        public int NumPostsToList { get; set; }

        public string DatabaseConnection { get; set; }

        public int CacheLengthInSeconds { get; set; }
    }
}