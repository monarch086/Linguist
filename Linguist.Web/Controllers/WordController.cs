using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Linguist.DataLayer.Model;
using Linguist.Services.Interfaces;
using System.Net;
using System.Text;

namespace Linguist.Web.Controllers
{
    public class WordController : Controller
    {
        private readonly IAccountsService _accountsService;
        private readonly IUsersService _userService;
        private readonly IWordsService _wordsService;
        private readonly ICategoriesService _categoriesService;

        public WordController(IAccountsService accountsService, IUsersService userService, IWordsService wordsService, ICategoriesService categoriesService)
        {
            _userService = userService;
            _wordsService = wordsService;
            _accountsService = accountsService;
            _categoriesService = categoriesService;
        }

        [HttpGet]
        public ActionResult Add()
        {
            ViewBag.CategoriesListItems = GetUserCategoriesAsSelectList();

            return View();
        }

        [HttpPost]
        public ActionResult Add(int categoryId, string originalWord, string translation)
        {
            var login = _accountsService.GetUserName(System.Web.HttpContext.Current);

            User user = _userService.GetUserByLogin(login);

            if (_wordsService.WordIsAlreadySaved(login, originalWord))
            {
                var word = _userService.GetUserWords(login).FirstOrDefault(w => w.OriginalWord.Equals(originalWord));
                var categoriesOfWord = _wordsService.GetCategoriesOfWord(word.WordId);
                StringBuilder sb = new StringBuilder();
                foreach (var category in categoriesOfWord)
                {
                    sb.Append(category.CategoryName + ", ");
                }
                sb.Remove(sb.Length - 2, 2);
                var operationMessage = $"Слово {originalWord} не сохранено, так как такое слово уже есть в словарях: {sb}";
                return Redirect(Url.Action("MyWords", "Home", new { categoryId, message = operationMessage }));
            }

            Word _word = new Word
            {
                UserId = user.UserId,
                OriginalWord = originalWord,
                Translation = translation,
                DateAdded = DateTime.Now,
                RememberIndex = 0
            };

            if (_wordsService.AddWord(_word, categoryId))
            {
                var operationMessage = $"Слово {originalWord} сохранено";
                return Redirect(Url.Action("MyWords", "Home", new {categoryId, message = operationMessage}));
            }
            else
            {
                var operationMessage = $"Слово {originalWord} не удалось сохранить";
                return Redirect(Url.Action("MyWords", "Home", new { categoryId, message = operationMessage }));
            }
        }

        public ActionResult Remove(int wordId, string returnUrl)
        {
            if (_wordsService.RemoveWord(wordId))
                return Redirect(returnUrl);

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Exception: word can not be deleted");
        }

        public ActionResult Edit(int wordId, string returnUrl)
        {
            Word word = _wordsService.GetWordById(wordId);

            if (word != null)
            {
                var categoriesIds = _categoriesService.GetCategoriesIdsByWordId(wordId);
                ViewBag.CategoriesListItems = GetUserCategoriesAsSelectList(categoriesIds.FirstOrDefault());
                return View("~/Views/Word/Add.cshtml", word);
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Exception: word can not be edited");
        }

        private IEnumerable<SelectListItem> GetUserCategoriesAsSelectList(int selectedCategoryId = 0)
        {
            var login = _accountsService.GetUserName(System.Web.HttpContext.Current);
            return _userService.GetUserCategories(login).Select(i => new SelectListItem
            {
                Text = i.CategoryName,
                Value = i.CategoryId.ToString(),
                Selected = i.CategoryId == selectedCategoryId
            }).ToList();
        }
    }
}