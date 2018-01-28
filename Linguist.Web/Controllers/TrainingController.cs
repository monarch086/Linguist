using System;
using System.Linq;
using System.Web.Mvc;
using Linguist.Services.Interfaces;
using Linguist.Web.Extensions;

namespace Linguist.Web.Controllers
{
    public class TrainingController : Controller
    {
        private readonly IAccountsService _accountsService;
        private readonly IUsersService _userService;
        private readonly IWordsService _wordsService;

        public TrainingController(IAccountsService accountsService, IUsersService userService, IWordsService wordsService, ICategoriesService categoriesService)
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
            words = words.OrderBy(item => rnd.Next()).TransformStarSigns().ToList();

            return View("~/Views/Training/Training.cshtml", words);
        }

        public ActionResult CategoryWords(int categoryId)
        {
            var words = _wordsService.GetWordsByCategory(categoryId).ToList();

            var rnd = new Random();
            words = words.OrderBy(item => rnd.Next()).TransformStarSigns().ToList();

            return View("~/Views/Training/Training.cshtml", words);
        }
    }
}