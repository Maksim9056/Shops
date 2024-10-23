using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopClassLibrary.ModelShop
{
    public interface IUserService
    {
        Task<string> AuthenticateUserAsync(string email, string password);
        Task<User> RegisterUserAsync(User user);
        Task<User> User_Product(long Id , string email);


    }
}
