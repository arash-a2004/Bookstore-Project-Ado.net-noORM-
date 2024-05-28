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
        public OrderDetail GetOrderDetails(string Query);
        public Cart GetCartDetails(string Query);
        public List<OrderDetail> GetListOfOrders(string Query);
        public int InsertMultipleRecords(Book book,List<BookCategory> bookCategories);
        public Store GetStoreById(string Query);
        public List<Store> GetListOfStores(string Query);
        public Book GetBookById(int bookId);
        public int DMLTranaction(string Query);


    }
}
