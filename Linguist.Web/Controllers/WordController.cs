using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Linguist.DataLayer.Model;
using Linguist.Services.Interfaces;
using System.Net;

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

            Word _word = new Word
            {
                UserId = user.UserId,
                OriginalWord = originalWord,
                Translation = translation,
                DateAdded = DateTime.Now,
                RememberIndex = 0
            };

            _wordsService.AddWord(_word, categoryId);
            return Redirect(Url.Action("MyWords", "Home"));
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