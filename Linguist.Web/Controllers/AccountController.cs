using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Linguist.Services;
using Linguist.DataLayer.Model;

namespace Linguist.Web.Controllers
{
    public class AccountController : Controller
    {
        private UserService _userService;

        public AccountController()
        {
            _userService = new UserService();
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
            int salt = _userService.ComputeSalt();

            User user = new User
            {
                Login = login,
                Salt = salt,
                Password = _userService.GetHashFromPassword(password, salt),
                DateAdded = DateTime.Now,
                IsAdmin = false
            };

            if (_userService.AddUser(user))
                return new HttpResponse();

            return View();
        }

        public ActionResult SignOut()
        {
            authProvider.SignOut();
            return Redirect(Url.Action("List", "Products"));
        }
    }
}