using BookStore.Entities;
using CookStore.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Net;

namespace BookStore.Services
{
    public class BookStoreServices : IBookStoreService
    {
        private readonly IConfiguration _configuration;
        private string _connectionString = string.Empty;
        public BookStoreServices(IConfiguration config) 
        {
            _configuration = config;
            _connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MyBookStore;Integrated Security=True";
        }

        public int DMLTranaction(string Query)
        {
            throw new NotImplementedException();
        }

        //finish
        public Book GetBookById(int bookId)
        {
            Book book = new();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "select * from [Book] where Id=@id";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", bookId);
                connection.Open();
                Console.WriteLine("connection is established");
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        book.Id = Convert.ToInt32(reader["Id"]);
                        book.Name = reader["Name"].ToString();
                        book.Description = reader["Description"].ToString();
                        book.ISBNNumber = reader["ISBNNumber"].ToString();
                        book.Price = Convert.ToDecimal(reader["Price"]);
                        book.BookImage = reader["PictureUri"].ToString();
                        book.BookAuthorId = Convert.ToInt32(reader["BookAuthorId"]);
                        book.BookStoreId = Convert.ToInt32(reader["BookStoreId"]);
                    }
                }
                connection.Close();
            }
            return book;
        }

        //finish
        public List<Book> GetListOfBooks()
        {
            List<Book> books = new List<Book>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "select * from [Book]";
                SqlCommand cmd = new SqlCommand(query, connection);
                connection.Open();
                Console.WriteLine("connection is established");
                using(SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
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
        //finish
        public List<Cart> GetCartDetailsByUserId(int userId)
        {
            List<Cart> carts = new();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM [Cart] WHERE Cart.UserId = @id;";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", userId);
                connection.Open();
                Console.WriteLine("connection is established");
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        carts.Add(new Cart
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            BookId = Convert.ToInt32(reader["BookId"]),
                            BookName = reader["Name"].ToString(),
                            Price = Convert.ToDecimal(reader["Price"]),
                            Quantity = Convert.ToInt32(reader["Quantity"]),
                            TotalAmount = Convert.ToDecimal(reader["TotalAmount"]),
                            UserId = Convert.ToInt32(reader["UserId"])
                        });
                    }
                }
                connection.Close();
            }
            return carts;
        }

        //Category  
        //finish
        public List<Category> GetListOfCategoriesByBookId(int BookId)
        {
            List<Category> categories = new();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT Category.Id , Category.Name FROM Category INNER JOIN BookCategory ON Category.Id = BookCategory.CategoryId WHERE BookId = @id";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", BookId);
                connection.Open();
                Console.WriteLine("connection is established");
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        categories.Add(new Category
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader["Name"].ToString()

                        });
                    }
                }
                connection.Close();
            }
            return categories;
        }
        //finish
        public Category GetListOfCategoriesByCategoryId(int CategoryId)
        {
            Category category = new();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Category WHERE Category.Id = @id";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", CategoryId);
                connection.Open();
                Console.WriteLine("connection is established");
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        category.Id = Convert.ToInt32(reader["Id"]);
                        category.Name = reader["Name"].ToString();
                    }
                }
                connection.Close();
            }
            return category;
        }
        //finish
        public List<Category> GetListOfCategories()
        {
            List<Category> categories = new();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "select * from [Category];";
                SqlCommand cmd = new SqlCommand(query, connection);
                connection.Open();
                Console.WriteLine("connection is established");
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        categories.Add(new Category
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader["Name"].ToString()

                        });
                    }
                }
                connection.Close();
            }
            return categories;
        }

        //Orders
        //finish
        public List<OrderDetail> GetListOfOrders(string Query)
        {
            List<OrderDetail> orders = new();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "select * from [Order]";
                SqlCommand cmd = new SqlCommand(query, connection);
                connection.Open();
                Console.WriteLine("connection is established");
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        orders.Add(new OrderDetail
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            BookId = Convert.ToInt32(reader["BookId"]),
                            BookName = reader["Name"].ToString(),
                            Price = Convert.ToDecimal(reader["Price"]),
                            Quantity = Convert.ToInt32(reader["Quantity"]),
                            TotalAmount = Convert.ToDecimal(reader["TotalAmount"]),
                            UserId = Convert.ToInt32(reader["UserId"]),
                            OrderDate = Convert.ToDateTime(reader["OrderDate"])
                        });
                    }
                }
                connection.Close();
            }
            return orders;
        }
        public OrderDetail GetOrderDetails(string Query)
        {
            throw new NotImplementedException();
        }



        //finish
        public Store GetStoreById(int id)
        {
            Store store = new();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "select * from [BookStore] where Id=@id";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", id);
                connection.Open();
                Console.WriteLine("connection is established");
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        store.Id = Convert.ToInt32(reader["Id"]);
                        store.Name = reader["Name"].ToString();
                    }
                }
                connection.Close();
            }
            return store;
        }

        //finish
        public List<Store> GetListOfStores()
        {
            List<Store> stores = new List<Store>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "select * from [BookStore]";
                SqlCommand cmd = new SqlCommand(query, connection);
                connection.Open();
                Console.WriteLine("connection is established");
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        stores.Add(new Store
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader["Name"].ToString(),
                        });
                    }
                }
                connection.Close();
            }
            return stores;
        }

        public int InsertMultipleRecords(Book book, List<BookCategory> bookCategories)
        {
            throw new NotImplementedException();
        }

        //finish    
        public List<BookAuthor> GetListOfBookAuthor()
        {
            List<BookAuthor> bookAuthors = new List<BookAuthor>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "select * from [BookAuthor]";
                SqlCommand cmd = new SqlCommand(query, connection);
                connection.Open();
                Console.WriteLine("connection is established");
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        bookAuthors.Add(new BookAuthor
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader["Name"].ToString()
                        });
                    }
                }
                connection.Close();
            }
            return bookAuthors;
        }

        //finish
        public BookAuthor GetBookAuthorById(int id)
        {
            BookAuthor bookauthor = new();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "select * from [BookAuthor] where Id=@id";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", id);
                connection.Open();
                Console.WriteLine("connection is established");
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        bookauthor.Id = Convert.ToInt32(reader["Id"]);
                        bookauthor.Name = reader["Name"].ToString();
                    }
                }
                connection.Close();
            }
            return bookauthor;
        }

    }
}
