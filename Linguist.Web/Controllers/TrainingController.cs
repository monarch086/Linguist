using System.Linq;
using System.Web.Mvc;
using Linguist.Services.Interfaces;

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
            ViewBag.Words = words;

            return View();
        }

        public ActionResult CategoryWords(int categoryId)
        {
            return View();
        }
    }
}