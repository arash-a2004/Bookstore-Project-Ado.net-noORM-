using BookStore.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public class AuthenticationService : IAuthentication
    {
        private string _connectionString = string.Empty;
        public AuthenticationService()
        {
            _connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MyBookStore;Integrated Security=True";
        }
        public int AddRole(string rolename) 
        {
            using (SqlConnection con = new SqlConnection(_connectionString)) 
            {
                con.Open();
                //TODO
                string query = "INSERT INTO Role (Name) VALUES (@name);";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@name", rolename);
                    int result = cmd.ExecuteNonQuery();
                    con.Close();
                    return result;
                }
            }
        }


        public List<Role> GetAllRoles()
        {
            List<Role> roles = new List<Role>();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                string query = "SELECT * FROM [Role];";
                SqlCommand cmd = new SqlCommand(query, con);
                Console.WriteLine("connection is established");
                using (SqlDataReader reader = cmd.ExecuteReader()) 
                {
                    while (reader.Read()) 
                    {
                        roles.Add(new Role
                        {
                            Id=Convert.ToInt32(reader["Id"]),
                            Name = reader["Name"].ToString()
                        });
                    }
                }
                con.Close();
            }
            return roles;
        }

        public int AddUser(AuthenticatedUser user)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                string query = "INSERT INTO UserTable (UserName, Email, Password, Name, ContactNumber, Address, RoleId) VALUES (@userName, @email, @password, @name, @contactNumber, @address, @roleId);";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@userName", user.UserName);
                    cmd.Parameters.AddWithValue("@email", user.Email);
                    cmd.Parameters.AddWithValue("@password", user.Password);
                    cmd.Parameters.AddWithValue("@name", user.Name);
                    cmd.Parameters.AddWithValue("@contactNumber", user.ContactNumber);
                    cmd.Parameters.AddWithValue("@address", user.Address);
                    cmd.Parameters.AddWithValue("@roleId",2);
                    int result = cmd.ExecuteNonQuery();
                    con.Close();
                    return result;
                }
            }
        }

        public AuthenticatedUser checkUser(string username, string password)
        {
            AuthenticatedUser authenticatedUser = new();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                string query = "SELECT TOP 1 * FROM UserTable WHERE UserName=@userName AND Password = @password ;";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@userName", username);
                    cmd.Parameters.AddWithValue("@password", password);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            authenticatedUser.Id = Convert.ToInt32(reader["Id"]);
                            authenticatedUser.UserName = reader["UserName"].ToString();
                            authenticatedUser.Email = reader["Email"].ToString();
                            authenticatedUser.Password = reader["Password"].ToString();
                            authenticatedUser.Name = reader["Name"].ToString();
                            authenticatedUser.ContactNumber = reader["ContactNumber"].ToString();
                            authenticatedUser.Address = reader["Address"].ToString();
                            authenticatedUser.RoleId = Convert.ToInt32(reader["RoleId"]);
                        }
                    }
                    con.Close();
                }
                return authenticatedUser;
            }

        }

        public bool checkUserExist(string username, string password)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                bool userExists;
                con.Open();
                string query = "SELECT TOP 1 1 FROM UserTable WHERE UserName=@userName AND Password = @password;";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@userName", username);
                    cmd.Parameters.AddWithValue("@password", password);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        userExists = reader.HasRows;
                        Console.WriteLine(userExists ? "User exists." : "User does not exist.");
                    }
                    con.Close();
                }
                return userExists;
            }
        }

        public Role GetRole(int roleId)
        {
            var role = new Role();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                string query = "SELECT * FROM [Role] WHERE Id=@id ;";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", roleId);
                    var reader= cmd.ExecuteReader();
                    if(reader.Read())
                    {
                        role.Id = Convert.ToInt32(reader["Id"]); 
                        role.Name = reader["Name"].ToString();
                    }
                    con.Close();
                }
                return role;
            }
        }
    }
}
