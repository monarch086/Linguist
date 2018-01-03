using System;
using System.Web.Mvc;
using Linguist.DataLayer.Model;
using Linguist.Services.Interfaces;

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
            return View();
        }

        [HttpPost]
        public ActionResult Add(string word, string translation, int categoryId)
        {
            var login = _accountsService.GetUserName(System.Web.HttpContext.Current);

            User user = _userService.GetUserByLogin(login);

            Word _word = new Word
            {
                UserId = user.UserId,
                OriginalWord = word,
                Translation = translation,
                DateAdded = DateTime.Now
            };

            _wordsService.AddWord(_word, categoryId);
            return Redirect(Url.Action("MyWords", "Home"));
        }
    }
}