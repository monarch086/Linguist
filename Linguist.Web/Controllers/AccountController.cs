using System;
using System.Net;
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
                return Redirect(Url.Action("MyWords", "Home", new {login}));
            
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
                return Redirect(Url.Action("MyWords", "Home", new { login }));

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Exception: unable to add new user");
        }

        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return Redirect(Url.Action("Start", "Account"));
        }
    }
}