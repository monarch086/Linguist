using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Linguist.Services.Interfaces;

namespace Linguist.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUsersService _userService;

        public HomeController(IUsersService userService)
        {
            _userService = userService;
        }

        public ActionResult MyWords(string login)
        {
            var words = _userService.GetUserWords(login);

            return View(words);
        }

        public ActionResult Train()
        {
            return View();
        }

        public ActionResult Test()
        {
            return View();
        }

        public ActionResult Statistics()
        {
            return View();
        }

        public ActionResult Options()
        {
            return View();
        }
    }
}