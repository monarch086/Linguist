using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Linguist.Services.Interfaces;

namespace Linguist.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUsersService _userService;

        public HomeController(IUsersService userService, AccountController accountController)
        {
            _userService = userService;

            
            //get instance of AccountController
            //_accountController = DependencyResolver.Current.GetService<AccountController>();
            //_accountController.ControllerContext = new ControllerContext(this.Request.RequestContext, _accountController);
        }

        public ActionResult MyWords()
        {
            var login = GetUserName();

            if (string.IsNullOrEmpty(login))
                return Redirect(Url.Action("Start", "Account"));

            var words = _userService.GetUserWords(login);

            return View(words);
        }

        public ActionResult Train()
        {
            return View();
        }

        public ActionResult Test()
        {
            return View();
        }

        public ActionResult Statistics()
        {
            return View();
        }

        public ActionResult Options()
        {
            return View();
        }

        private string GetUserName()
        {
            try
            {
                string cookieName = FormsAuthentication.FormsCookieName; //Find cookie name
                HttpCookie authCookie = HttpContext.Request.Cookies[cookieName]; //Get the cookie by it's name
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value); //Decrypt it
                return ticket.Name;
            }
            catch (NullReferenceException)
            {
                return string.Empty;
            }
        }
    }
}