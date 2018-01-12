using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Linguist.DataLayer.Model;
using Linguist.Services.Interfaces;
using Linguist.Web.Models;

namespace Linguist.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAccountsService _accountsService;
        private readonly IUsersService _userService;
        private readonly IWordsService _wordsService;
        private readonly ICategoriesService _categoriesService;

        public HomeController(IAccountsService accountsService, IUsersService userService, IWordsService wordsService, ICategoriesService categoriesService)
        {
            _accountsService = accountsService;
            _userService = userService;
            _wordsService = wordsService;
            _categoriesService = categoriesService;
        }

        public ActionResult MyWords(int categoryId = 0, string message = null)
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

            var wordsWithCategories = words.Select(w =>
                    new WordViewModel {Word = w, WordCategories = _categoriesService.GetCategoriesByWordId(w.WordId)})
                .ToList();

            if (message != null)
            {
                ViewBag.Message = message;
            }

            if (Request.Browser.IsMobileDevice)
                return View("~/Views/Home/MyWords.Mobile.cshtml", wordsWithCategories);

            return View(wordsWithCategories);
        }
    }
}