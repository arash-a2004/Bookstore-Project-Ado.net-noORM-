using BookStore.Entities;
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
        [HttpPost]
        public IActionResult Edit(Cart cart)
        {
            var totalAmount = (cart.TotalAmount) * (cart.Price);
            int res = _bookStoreService.UpdateCart(cart.Id,totalAmount ,cart.Quantity);
            if(res > 0)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Delete(Cart cart)
        {
            int result = _bookStoreService.DeleteCartByUserId(cart.Id);
            if (result > 0) 
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
