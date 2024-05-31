using BookStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookStore.Services
{
    public interface IBookStoreService
    {
        public List<Book> GetListOfBooks();
        public List<Category> GetListOfCategoriesByBookId(int BookId);
        public Category GetListOfCategoriesByCategoryId(int CategoryId);
        public List<Category> GetListOfCategories();
        public OrderDetail GetOrderDetails(string Query);
        public List<OrderDetail> GetListOfOrders();
        public int InsertMultipleRecords(Book book,List<BookCategory> bookCategories);
        public Store GetStoreById(int id);
        public List<Store> GetListOfStores();
        public Book GetBookById(int bookId);
        public int DMLTranaction(string Query);
        public List<BookAuthor> GetListOfBookAuthor();
        public BookAuthor GetBookAuthorById(int id);
        //Cart Details
        public List<Cart> GetCartDetailsByUserId(int userId);
        public int saveBookInCart(Cart cart);
        public Cart GetCartById(int cartId);
    }
}
