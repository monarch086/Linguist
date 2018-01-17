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
        private readonly int pageSize = 10;

        public HomeController(IAccountsService accountsService, IUsersService userService, IWordsService wordsService, ICategoriesService categoriesService)
        {
            _accountsService = accountsService;
            _userService = userService;
            _wordsService = wordsService;
            _categoriesService = categoriesService;
        }

        public ActionResult MyWords(int page = 1, int categoryId = 0, string message = null)
        {
            var login = _accountsService.GetUserName(System.Web.HttpContext.Current);

            if (string.IsNullOrEmpty(login))
                return Redirect(Url.Action("Start", "Account"));

            if (page < 1)
                page = 1;

            IEnumerable<Word> words;
            int totalWordsCount;

            if (categoryId == 0)
            {
                words = _userService.GetUserWords(login)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize).ToList();

                totalWordsCount = _userService.GetUserWords(login).Count();
            }

            else
            {
                words = _wordsService.GetWordsByCategory(categoryId)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize).ToList();

                totalWordsCount = _wordsService.GetWordsByCategory(categoryId).Count();
            }

            var model = GetMyWordsModelFromWords(words, page, totalWordsCount, categoryId, message);

            if (Request.Browser.IsMobileDevice)
                return View("~/Views/Home/MyWords.Mobile.cshtml", model);

            return View(model);
        }

        public ActionResult SearchWords(string word, int page = 1)
        {
            var login = _accountsService.GetUserName(System.Web.HttpContext.Current);

            var words = _userService.GetUserWords(login)
                .Where(w => w.OriginalWord.ToLower().Contains(word) || w.Translation.ToLower().Contains(word))
                .Skip((page - 1) * pageSize)
                .Take(pageSize).ToList();

            var model = GetMyWordsModelFromWords(words, 0, 0, 0, null);

            return View(model);
        }

        public ActionResult SearchPager(string word, int page = 1)
        {
            var login = _accountsService.GetUserName(System.Web.HttpContext.Current);

            var totalWordsCount = _userService
                .GetUserWords(login)
                .Count(w => w.OriginalWord.ToLower().Contains(word) || w.Translation.ToLower().Contains(word));

            var pageModel = new PagingInfo
            {
                CurrentPage = page,
                ItemsPerPage = pageSize,
                TotalItems = totalWordsCount
            };

            return View(pageModel);
        }

        private MyWordsModel GetMyWordsModelFromWords(IEnumerable<Word> words, int page, int totalWordsCount, int categoryId, string message)
        {
            var wordsWithCategories = words.Select(w =>
                    new WordViewModel { Word = w, WordCategories = _categoriesService.GetCategoriesByWordId(w.WordId) })
                .ToList();

            var model = new MyWordsModel
            {
                WordsWithCategories = wordsWithCategories,
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = totalWordsCount
                },
                CurrentCategoryId = categoryId,
                Message = message
            };

            return model;
        }
    }
}