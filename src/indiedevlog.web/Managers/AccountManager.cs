using System.Linq;

using indiedevlog.web.EFModel;
using indiedevlog.web.EFModel.Objects.Tables;
using indiedevlog.web.Settings;

namespace indiedevlog.web.Managers
{
    public class AccountManager : BaseManager
    {
        public AccountManager(GlobalSettings globalSettings) : base(globalSettings)
        {
        }

        public Users AttemptLogin(string username, string password)
        {
            using (var eFactory = new EntityFactory(GlobalSettings.DatabaseConnection))
            {
                var passwordHash = hashString(password);

                return
                    eFactory.Users.FirstOrDefault(a => a.Username == username && a.Password == passwordHash && a.Active);
            }
        }

        public bool AttemptRegister(string username, string password, string displayName)
        {
            using (var eFactory = new EntityFactory(GlobalSettings.DatabaseConnection))
            {
                if (
                    eFactory.Users.Any(
                        a => a.Active && (a.Username == username || a.DisplayName == displayName)))
                {
                    return false;
                }

                var user = new Users
                {
                    IsConfirmed = true,
                    Password = hashString(password),
                    Username = username,
                    DisplayName = displayName
                };

                eFactory.Users.Add(user);
                eFactory.SaveChanges();
            }

            return true;
        }
    }
}