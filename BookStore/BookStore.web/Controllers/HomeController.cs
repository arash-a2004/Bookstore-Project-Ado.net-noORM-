using BookStore.web.Models;
using CookStore.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BookStore.web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IBookStoreService _bookService;

        public HomeController(ILogger<HomeController> logger, IBookStoreService bookService)
        {
            _logger = logger;
            _bookService = bookService;

        }

        public IActionResult Index()
        {
            var books = _bookService.GetListOfBooks();
            return View(books);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
