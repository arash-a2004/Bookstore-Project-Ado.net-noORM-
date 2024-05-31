using BookStore.web.Filters;
using CookStore.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.web.Controllers
{
    public class CartsController : Controller
    {
        private IBookStoreService _bookStoreService;
        public CartsController(IBookStoreService bookStoreService)
        {
            _bookStoreService = bookStoreService;
        }
        [CheckSession("userId")]
        public IActionResult Index()
        {
            var carts = _bookStoreService.GetCartDetailsByUserId((int)HttpContext.Session.GetInt32("userId"));
            return View(carts);
        }
        [HttpGet]
        [CheckSession("userId")]
        public IActionResult Edit(int id) 
        {
            var cart = _bookStoreService.GetCartById(id);
            return View(cart);
        }
    }
}
