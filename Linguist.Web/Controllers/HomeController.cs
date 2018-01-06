using System.Collections.Generic;
using System.Web.Mvc;
using Linguist.DataLayer.Model;
using Linguist.Services.Interfaces;

namespace Linguist.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAccountsService _accountsService;
        private readonly IUsersService _userService;
        private readonly IWordsService _wordsService;

        public HomeController(IAccountsService accountsService, IUsersService userService, IWordsService wordsService)
        {
            _accountsService = accountsService;
            _userService = userService;
            _wordsService = wordsService;
        }

        public ActionResult MyWords(int categoryId = 0)
        {
            var login = _accountsService.GetUserName(System.Web.HttpContext.Current);

            if (string.IsNullOrEmpty(login))
                return Redirect(Url.Action("Start", "Account"));

            IEnumerable<Word> words;

            if (categoryId == 0)
                words = _userService.GetUserWords(login);

            else
            {
                words = _wordsService.GetWordsByCategory(categoryId);
            }

            return View(words);
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