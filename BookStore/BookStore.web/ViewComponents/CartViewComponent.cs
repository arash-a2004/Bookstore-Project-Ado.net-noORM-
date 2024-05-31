using CookStore.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.web.ViewComponents
{
    public class CartViewComponent : ViewComponent
    {
        private IBookStoreService _bookStoreService;

        public CartViewComponent(IBookStoreService bookStoreService)
        {
            _bookStoreService = bookStoreService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (HttpContext.Session.GetInt32("userId") != null)
            {

                if (HttpContext.Session.GetString("sessionCart") != null)
                {
                    return View(HttpContext.Session.GetInt32("sessionCart"));
                }
                else
                {
                    int userid = (int)HttpContext.Session.GetInt32("userId");
                    HttpContext.Session.SetInt32("sessionCart", _bookStoreService.GetCartDetailsByUserId(userid).Count());
                    return View(HttpContext.Session.GetInt32("sessionCart"));
                }
            }
            else 
            {
                HttpContext.Session.Clear();
                return View(0);
            }
        }
    }
}
