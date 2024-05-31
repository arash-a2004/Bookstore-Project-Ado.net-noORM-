using BookStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public interface IAuthentication
    {
        int AddRole(string rolename);
        List<Role> GetAllRoles();
        int AddUser(AuthenticatedUser user);
        AuthenticatedUser checkUser(string username , string password);
        bool checkUserExist(string username , string password);
        public Role GetRole(int roleId);
    }
}
