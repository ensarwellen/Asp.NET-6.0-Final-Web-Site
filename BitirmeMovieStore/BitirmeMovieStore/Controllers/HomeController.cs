using BitirmeMovieStore.Models;
using BitirmeMovieStore.Repositories.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BitirmeMovieStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMovieService _movieService;

        //private readonly ILogger<HomeController> _logger; ILogger<HomeController> logger _logger = logger;

        public HomeController(IMovieService movieService)
        {
            _movieService = movieService;
        }
        [Authorize]
        public IActionResult Index(string term="", int currentPage = 1)
        {
            var movies = _movieService.List(term,true,currentPage);
            return View(movies);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
        public IActionResult MovieDetail(int movieId)
        {
            var movie = _movieService.GetById(movieId);
            return View(movie);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}