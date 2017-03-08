using System;
using System.Security.Cryptography;
using System.Text;

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

        protected string hashString(string input)
        {
            using (var algorithm = SHA512.Create())
            {
                var hash = algorithm.ComputeHash(Encoding.ASCII.GetBytes(input));

                var hashbytes = algorithm.ComputeHash(hash);

                return Convert.ToBase64String(hashbytes);
            }
        }
    }
}