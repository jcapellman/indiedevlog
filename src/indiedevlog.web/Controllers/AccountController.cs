using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using indiedevlog.web.EFModel;
using indiedevlog.web.EFModel.Objects;
using indiedevlog.web.EFModel.Objects.Tables;
using indiedevlog.web.Models;
using indiedevlog.web.Settings;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace indiedevlog.web.Controllers
{
    public class AccountController : BaseController
    {
        public AccountController(IOptions<GlobalSettings> globalSettings) : base(globalSettings.Value)
        {
        }

        public ActionResult Index()
        {
            var model = new LoginModel();

            return View(model);
        }

        public ActionResult Register()
        {
            var model = new RegisterModel();

            return View(model);
        }

        public async Task<ActionResult> AttemptLogin(LoginModel model)
        {
            using (var eFactory = new EntityFactory(_globalSettings.DatabaseConnection))
            {
                var passwordHash = hashString(model.Password);

                var userMatch =
                    eFactory.Users.FirstOrDefault(a => a.Username == model.Username && a.Password == passwordHash && a.Active);

                if (userMatch != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim("username", model.Username),
                        new Claim("userid", userMatch.ID.ToString())
                    };

                    var id = new ClaimsIdentity(claims, "password");
                    var principal = new ClaimsPrincipal(id);

                    await HttpContext.Authentication.SignInAsync("CookieMiddleware", principal);

                    return RedirectToAction("Index", "Home");
                }

                model.ErrorMessage = "Incorrect username or password";

                return View("Index", model);
            }
        }

        public async Task<ActionResult> Logout()
        {
            await HttpContext.Authentication.SignOutAsync("CookieMiddleware");

            return RedirectToAction("Index", "Home");
        }

        private string hashString(string input)
        {
            using (var algorithm = SHA512.Create())
            {
                var hash = algorithm.ComputeHash(Encoding.ASCII.GetBytes(input));

                var hashbytes = algorithm.ComputeHash(hash);

                return Convert.ToBase64String(hashbytes);
            }
        }

        [HttpPost]
        public ActionResult AttemptRegister(RegisterModel model)
        {
            using (var eFactory = new EntityFactory(_globalSettings.DatabaseConnection))
            {
                if (eFactory.Users.Any(a => a.Active && (a.Username == model.Username || a.DisplayName == model.DisplayName)))
                {
                    model.ErrorMessage = "Username or Display Name is already taken";

                    return View("Register", model);
                }

                var user = new Users
                {
                    IsConfirmed = true,
                    Password = hashString(model.Password),
                    Username = model.Username,
                    DisplayName = model.DisplayName
                };
                
                eFactory.Users.Add(user);
                eFactory.SaveChanges();

                return RedirectToAction("Index", "Home");
            }
        }
    }
}