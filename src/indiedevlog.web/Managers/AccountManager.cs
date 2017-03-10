using System.Linq;

using indiedevlog.web.Common;
using indiedevlog.web.EFModel;
using indiedevlog.web.EFModel.Objects.Tables;
using indiedevlog.web.Objects.Users;
using indiedevlog.web.Settings;

namespace indiedevlog.web.Managers
{
    public class AccountManager : BaseManager
    {
        public AccountManager(GlobalSettings globalSettings) : base(globalSettings)
        {
        }

        public ReturnSet<UserResponseItem> AttemptLogin(string username, string password)
        {
            using (var eFactory = new EntityFactory(GlobalSettings.DatabaseConnection))
            {
                var passwordHash = HashString(password);

                var user = eFactory.Users.FirstOrDefault(a => a.Username == username && a.Password == passwordHash && a.Active);

                if (user == null)
                {
                    return new ReturnSet<UserResponseItem>(null, $"No match on {username} and {password}");
                }

                return new ReturnSet<UserResponseItem>(new UserResponseItem
                {
                    UserID = user.ID,
                    DisplayName = user.DisplayName
                });
            }
        }

        public ReturnSet<bool> AttemptRegister(string username, string password, string displayName)
        {
            using (var eFactory = new EntityFactory(GlobalSettings.DatabaseConnection))
            {
                if (
                    eFactory.Users.Any(
                        a => a.Active && (a.Username == username || a.DisplayName == displayName)))
                {
                    return new ReturnSet<bool>(false);
                }

                var user = new Users
                {
                    IsConfirmed = true,
                    Password = HashString(password),
                    Username = username,
                    DisplayName = displayName
                };

                eFactory.Users.Add(user);
                eFactory.SaveChanges();
            }

            return new ReturnSet<bool>(true);
        }
    }
}