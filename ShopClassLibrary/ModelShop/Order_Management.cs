using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopClassLibrary.ModelShop
{
    internal interface Order_Management
    {
       Task Add(Order order);
        Task Update(Order order);

        Task Show(User user);
        Task Status(User user);


        Task Delete(Order order);

    }
}
