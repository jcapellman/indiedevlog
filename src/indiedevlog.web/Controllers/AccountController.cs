using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

using indiedevlog.web.Managers;
using indiedevlog.web.Models;
using indiedevlog.web.Settings;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace indiedevlog.web.Controllers
{
    [ResponseCache(CacheProfileName = "Never")]
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
            var userMatch = new AccountManager(_globalSettings).AttemptLogin(model.Username, model.Password);

            if (!userMatch.HasError && userMatch.ObjectValue != null)
            {
                var claims = new List<Claim>
                    {
                        new Claim("username", model.Username),
                        new Claim("userid", userMatch.ObjectValue.UserID.ToString())
                    };

                var id = new ClaimsIdentity(claims, "password");
                var principal = new ClaimsPrincipal(id);

                await HttpContext.Authentication.SignInAsync("CookieMiddleware", principal);

                return RedirectToAction("Index", "Home");
            }

            model.ErrorMessage = userMatch.ErrorException;

            return View("Index", model);            
        }

        public async Task<ActionResult> Logout()
        {
            await HttpContext.Authentication.SignOutAsync("CookieMiddleware");

            return RedirectToAction("Index", "Home");
        }
        
        [HttpPost]
        public ActionResult AttemptRegister(RegisterModel model)
        {
            var registrationResponse = new AccountManager(_globalSettings).AttemptRegister(model.Username,
                model.Password, model.DisplayName);

            if (!registrationResponse.HasError)
            {
                return RedirectToAction("Index", "Home");
            }

            model.ErrorMessage = "Username or Display Name is already taken";

            return View("Register", model);
        }
    }
}