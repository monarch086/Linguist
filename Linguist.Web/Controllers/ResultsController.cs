using Linguist.Services.Interfaces;
using System.Web.Mvc;

namespace Linguist.Web.Controllers
{
    public class ResultsController : Controller
    {
        private readonly IAccountsService _accountsService;
        private readonly IUsersService _userService;
        private readonly IWordsService _wordsService;

        public ResultsController(IAccountsService accountsService, IUsersService userService, IWordsService wordsService)
        {
            _accountsService = accountsService;
            _userService = userService;
            _wordsService = wordsService;
        }

        public ActionResult Statistics()
        {
            var login = _accountsService.GetUserName(System.Web.HttpContext.Current);

            if (string.IsNullOrEmpty(login))
                return Redirect(Url.Action("Start", "Account"));

            return View();
        }
    }
}