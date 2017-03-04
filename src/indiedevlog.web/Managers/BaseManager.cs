using indiedevlog.web.Settings;

namespace indiedevlog.web.Managers
{
    public class BaseManager
    {
        protected GlobalSettings GlobalSettings;

        public BaseManager(GlobalSettings globalSettings)
        {
            GlobalSettings = globalSettings;
        }
    }
}