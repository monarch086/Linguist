using System.Web.Mvc;
using Linguist.Services.Interfaces;

namespace Linguist.Web.Controllers
{
    public class OptionsController : Controller
    {
        private readonly IAccountsService _accountsService;
        private readonly IUsersService _userService;
        private readonly IWordsService _wordsService;
        private readonly ICategoriesService _categoriesService;
        private readonly ILogsService _logsService;

        public OptionsController(IAccountsService accountsService, IUsersService userService, IWordsService wordsService, ICategoriesService categoriesService, ILogsService logsService)
        {
            _accountsService = accountsService;
            _userService = userService;
            _wordsService = wordsService;
            _categoriesService = categoriesService;
            _logsService = logsService;
        }

        public ActionResult Main()
        {
            return View();
        }

        public void ChangeAllQuotesToStars()
        {
            var context = System.Web.HttpContext.Current;
            var login = _accountsService.GetUserName(context);

            var words = _userService.GetUserWords(login);

            foreach (var word in words)
            {
                word.OriginalWord = word.OriginalWord.Replace("'","*");
                word.OriginalWord = word.OriginalWord.Replace("**", "*");
                _wordsService.EditWord(word);
            }
        }

        public void ShiftAllStarsLeft()
        {
            var context = System.Web.HttpContext.Current;
            var login = _accountsService.GetUserName(context);

            var words = _userService.GetUserWords(login);

            foreach (var word in words)
            {
                for (int i = 0; i < word.OriginalWord.Length; i++)
                {
                    if (word.OriginalWord[i] == '*')
                    {
                        word.OriginalWord = word.OriginalWord.Remove(i, 1);
                        word.OriginalWord = word.OriginalWord.Insert(i - 1, "*");
                    }
                }

                _wordsService.EditWord(word);
            }
        }
    }
}