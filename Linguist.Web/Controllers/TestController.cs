using System;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Linguist.DataLayer.Model;
using Linguist.Services.Interfaces;
using Linguist.Web.Extensions;

namespace Linguist.Web.Controllers
{
    public class TestController : Controller
    {
        private readonly IAccountsService _accountsService;
        private readonly IUsersService _userService;
        private readonly IWordsService _wordsService;
        private readonly IResultsService _resultsService;

        public TestController(IAccountsService accountsService, IUsersService userService, IWordsService wordsService, ICategoriesService categoriesService, IResultsService resultsService)
        {
            _userService = userService;
            _wordsService = wordsService;
            _accountsService = accountsService;
            _resultsService = resultsService;
        }

        public ActionResult Main()
        {
            return View();
        }

        public ActionResult AllWords(bool showForeign = true)
        {
            var login = _accountsService.GetUserName(System.Web.HttpContext.Current);
            var words = _userService.GetUserWords(login).ToList();

            var rnd = new Random();

            var wordsFrom0To3 = words
                .Where(w => w.RememberIndex < 4)
                .OrderBy(item => rnd.Next())
                .Take(20).ToList();

            var wordsFrom4To7 = words
                .Where(w => w.RememberIndex >= 4 && w.RememberIndex < 8)
                .OrderBy(item => rnd.Next())
                .Take(10).ToList();

            var wordsFrom8To9 = words
                .Where(w => w.RememberIndex >= 8)
                .OrderBy(item => rnd.Next())
                .Take(5).ToList();

            words.Clear();
            words.AddRange(wordsFrom0To3);
            words.AddRange(wordsFrom4To7);
            words.AddRange(wordsFrom8To9);

            words.TransformStarSigns();

            //ViewBag.showForeign = showForeign;

            return View("~/Views/Test/Test.cshtml", words);
        }

        public ActionResult CategoryWords(int categoryId)
        {
            var words = _wordsService.GetWordsByCategory(categoryId).ToList();

            var rnd = new Random();
            words = words.OrderBy(item => rnd.Next()).ToList();

            words.TransformStarSigns();

            return View("~/Views/Test/Test.cshtml", words);
        }

        public void SaveTestResults(int[] rightWords, int[] wrongWords)
        {
            _wordsService.IncreaseRememberIndex(rightWords);
            _wordsService.DecreaseRememberIndex(wrongWords);

            _resultsService.AddTestResult(System.Web.HttpContext.Current, rightWords, wrongWords);
        }
    }
}