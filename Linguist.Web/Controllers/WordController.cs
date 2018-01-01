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

        public WordController(IAccountsService accountsService, IUsersService userService, IWordsService wordsService)
        {
            _userService = userService;
            _wordsService = wordsService;
            _accountsService = accountsService;
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
                CategoryId = categoryId,
                DateAdded = DateTime.Now
            };

            _wordsService.AddWord(_word);
            return Redirect(Url.Action("MyWords", "Home", new { login }));
        }
    }
}