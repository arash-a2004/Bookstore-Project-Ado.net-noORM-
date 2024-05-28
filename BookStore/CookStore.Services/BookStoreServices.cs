using BookStore.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookStore.Services
{
    public class BookStoreServices : IBookStoreService
    {
        private string _connectionString = string.Empty;
        public BookStoreServices(IConfiguration config) 
        {
            _connectionString = config["ConnectionStrings:DefaultConnection"];
        }

        public int DMLTranaction(string Query)
        {
            throw new NotImplementedException();
        }

        public Book GetBookById(int bookId)
        {
            throw new NotImplementedException();
        }

        public Cart GetCartDetails(string Query)
        {
            throw new NotImplementedException();
        }

        public List<Book> GetListOfBooks()
        {
            List<Book> books = new List<Book>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "select * from [Book]";
                SqlCommand cmd = new SqlCommand(query, connection);
                using(SqlDataReader reader = cmd.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        books.Add(new Book
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader["Name"].ToString(),
                            Description = reader["Description"].ToString(),
                            ISBNNumber = reader["ISBNNumber"].ToString(),
                            Price = Convert.ToDecimal(reader["Price"]),
                            BookImage = reader["PictureUri"].ToString(),
                            BookAuthorId = Convert.ToInt32(reader["BookAuthorId"]),
                            BookStoreId = Convert.ToInt32(reader["BookStoreId"])
                        });
                    }
                }
                connection.Close();
            }
            return books;
        }

        public List<Category> GetListOfCategoriesByBookId(int BookId)
        {
            throw new NotImplementedException();
        }

        public List<OrderDetail> GetListOfOrders(string Query)
        {
            throw new NotImplementedException();
        }

        public List<Store> GetListOfStores(string Query)
        {
            throw new NotImplementedException();
        }

        public OrderDetail GetOrderDetails(string Query)
        {
            throw new NotImplementedException();
        }

        public Store GetStoreById(string Query)
        {
            throw new NotImplementedException();
        }

        public int InsertMultipleRecords(Book book, List<BookCategory> bookCategories)
        {
            throw new NotImplementedException();
        }
    }
}
