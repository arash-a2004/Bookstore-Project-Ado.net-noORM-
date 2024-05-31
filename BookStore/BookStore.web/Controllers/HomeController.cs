using BookStore.Entities;
using BookStore.web.Filters;
using BookStore.web.Models;
using BookStore.web.ViewModels;
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
            List<BookviewModel> booksViewModel = new List<BookviewModel>();
            foreach (var book in books) 
            {
                booksViewModel.Add(new BookviewModel
                {
                    PictureUri = book.BookImage,
                    BookId = book.Id,
                    Price = book.Price,
                    Name = book.Name   
                });
            }
            return View(booksViewModel);
        }
        public IActionResult Details(int id) 
        {
            BookwithCategory vm = new BookwithCategory();
            var bookDetails = _bookService.GetBookById(id);
            if (bookDetails.BookAuthorId != 0) 
            {
                var bookAuther = _bookService.GetBookAuthorById(bookDetails.BookAuthorId);
                vm.Author = bookAuther; 
            }
            if(bookDetails.BookStoreId != 0)
            {
                var booksStore = _bookService.GetStoreById(bookDetails.BookStoreId);
                vm.BookStore = booksStore;  
            }
            vm.Categories = _bookService.GetListOfCategoriesByBookId(id);
            vm.BookId = bookDetails.Id;
            vm.Name = bookDetails.Name;
            vm.ISBNNumber = bookDetails.ISBNNumber;
            vm.Price = bookDetails.Price;
            vm.PictureUri = bookDetails.BookImage;
            vm.Description = bookDetails.Description;   
            return View(vm);
        }
        [CheckSession("userName")]
        [HttpPost]
        public IActionResult Cart(BookwithCategory vm) 
        {
            Cart cart = new Cart();
            cart.BookId = vm.BookId;
            cart.BookName = vm.Name;
            cart.Price = vm.Price;
            cart.Quantity = vm.Quantity;
            var total = (cart.Quantity)*(cart.Price);   
            cart.TotalAmount = total;
            cart.UserId = (int)HttpContext.Session.GetInt32("userId");
            int result = _bookService.saveBookInCart(cart);
            if (result > 0) 
            {
                HttpContext.Session.SetInt32("sessionCart", _bookService.GetCartDetailsByUserId(cart.UserId).Count());
                return RedirectToAction("Index","Carts");
            }
            return RedirectToAction("Index","Home");
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
