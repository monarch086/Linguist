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
            return View();
        }

        [HttpPost]
        public ActionResult Login(string login, string password)
        {
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(string login, string password)
        {
            int salt = _accountsService.ComputeSalt();

            User user = new User
            {
                Login = login,
                Salt = salt,
                Password = _accountsService.GetHashFromPassword(password, salt),
                DateAdded = DateTime.Now,
                IsAdmin = false
            };

            if (_userService.AddUser(user))
                return Redirect(Url.Action("MyWords", "Home"));

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Product not found");
        }

        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return Redirect(Url.Action("Start", "Account"));
        }
    }
}