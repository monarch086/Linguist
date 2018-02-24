using System;
using System.Linq;
using Linguist.Services.Interfaces;
using System.Web.Mvc;
using Linguist.Web.Models;

namespace Linguist.Web.Controllers
{
    public class ResultsController : Controller
    {
        private readonly IAccountsService _accountsService;
        private readonly IUsersService _userService;
        private readonly IResultsService _resultsService;

        public ResultsController(IAccountsService accountsService, IUsersService userService, IResultsService resultsService)
        {
            _accountsService = accountsService;
            _userService = userService;
            _resultsService = resultsService;
        }

        public ActionResult Statistics()
        {
            var login = _accountsService.GetUserName(System.Web.HttpContext.Current);

            if (string.IsNullOrEmpty(login))
                return Redirect(Url.Action("Start", "Account"));

            var userWords = _userService.GetUserWords(login);

            var model = new StatisticsViewModel
            {
                AllWords = userWords.Count(),
                WordsFrom0To3 = userWords.Count(w => w.RememberIndex < 4),
                WordsFrom4To7 = userWords.Count(w => w.RememberIndex >= 4 && w.RememberIndex < 8),
                WordsFrom8To9 = userWords.Count(w => w.RememberIndex >= 8),
                WordsAddedThisMonth = userWords.Count(w => w.DateAdded.Month == DateTime.Today.Month),
                TestsTakenLastWeek = _resultsService.GetTestsCountPerWeek(login)
            };

            return View(model);
        }
    }
}