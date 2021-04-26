using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mvc2LoginDemo.Controllers
{
    public class AccountController : Controller
    {

        public IActionResult Index()
        {
            return Content("index");
        }


        [Authorize]
        public IActionResult Add()
        {
            return Content("add");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Transfer()
        {
            return Content("transfer");
        }


    }
}