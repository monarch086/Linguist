using System;
using System.Web.Mvc;
using Linguist.DataLayer.Model;
using Linguist.Services.Interfaces;

namespace Linguist.Web.Controllers
{
    public class WordController : Controller
    {
        private readonly IUsersService _userService;
        private readonly IWordsService _wordsService;

        public WordController(IUsersService userService, IWordsService wordsService)
        {
            _userService = userService;
            _wordsService = wordsService;
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(string login, string word, string translation, int categoryId)
        {
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