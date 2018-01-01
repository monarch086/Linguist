using System;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Linguist.DataLayer.Model;
using Linguist.Services.Interfaces;

namespace Linguist.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUsersService _userService;
        private readonly IAccountsService _accountsService;

        public AccountController(IUsersService userService, IAccountsService accountsService)
        {
            _userService = userService;
            _accountsService = accountsService;
        }

        [HttpGet]
        public ActionResult Start()
        {
            var login = _accountsService.GetUserName(System.Web.HttpContext.Current);
            if (!string.IsNullOrEmpty(login))
                return Redirect(Url.Action("MyWords", "Home"));

            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string login, string password)
        {
            if (_accountsService.AuthenticateUser(login, password))
            {
                CreateCookie(login);
                return Redirect(Url.Action("MyWords", "Home"));
            }

            return Redirect(Url.Action("Start", "Account"));
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(string login, string name, string password)
        {
            if (login == null || password == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Exception: login and/or password not specified");

            int salt = _accountsService.ComputeSalt();

            User user = new User
            {
                Login = login,
                Name = name,
                Salt = salt,
                Password = _accountsService.GetHashFromPassword(password, salt),
                DateAdded = DateTime.Now,
                IsAdmin = false
            };

            if (_userService.AddUser(user))
            {
                CreateCookie(login);
                return Redirect(Url.Action("MyWords", "Home"));
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Exception: unable to add new user");
        }

        private void CreateCookie(string login)
        {
            // Create the authentication cookie
            HttpCookie authCookie = FormsAuthentication.GetAuthCookie(login, true);
            authCookie.Expires = DateTime.Now.AddDays(10);
            // Add the cookie to the response
            Response.Cookies.Add(authCookie);
        }

        public string GetUserName()
        {
            return _accountsService.GetUserName(System.Web.HttpContext.Current);
        }

        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return Redirect(Url.Action("Start", "Account"));
        }
    }
}