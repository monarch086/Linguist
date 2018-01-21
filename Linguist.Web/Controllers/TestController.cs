using System;
using System.Linq;
using System.Web.Mvc;
using Linguist.Services.Interfaces;

namespace Linguist.Web.Controllers
{
    public class TestController : Controller
    {
        private readonly IAccountsService _accountsService;
        private readonly IUsersService _userService;
        private readonly IWordsService _wordsService;

        public TestController(IAccountsService accountsService, IUsersService userService, IWordsService wordsService, ICategoriesService categoriesService)
        {
            _userService = userService;
            _wordsService = wordsService;
            _accountsService = accountsService;
        }

        public ActionResult Main()
        {
            return View();
        }

        public ActionResult AllWords()
        {
            var login = _accountsService.GetUserName(System.Web.HttpContext.Current);
            var words = _userService.GetUserWords(login).ToList();

            var rnd = new Random();
            words = words.OrderBy(item => rnd.Next()).ToList();

            return View("~/Views/Test/Test.cshtml", words);
        }
    }
}