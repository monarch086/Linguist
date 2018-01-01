using Linguist.Services.Interfaces;
using System.Web.Mvc;

namespace Linguist.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IUsersService _userService;
        private readonly ICategoriesService _categoriesService;

        public CategoryController(IUsersService userService, ICategoriesService categoriesService)
        {
            _userService = userService;
            _categoriesService = categoriesService;
        }


        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
    }
}