using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BitirmeMovieStore.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        //[Authorize(Roles = "admin,manager")]
        [Authorize(Roles = "admin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
