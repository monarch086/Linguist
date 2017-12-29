using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Linguist.Services;

namespace Linguist.Web.Controllers
{
    public class AccountController : Controller
    {
        private UserService _userService;

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

        public ActionResult Register()
        {
            return View();
        }
    }
}