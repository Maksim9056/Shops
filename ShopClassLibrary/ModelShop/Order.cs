using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopClassLibrary.ModelShop
{
    public class Order
    {
        public long Id { get; set; }
        public string OrdersName { get; set; }

        public IEnumerable<Product> Idproduct  { get; set; } = new List<Product>();

        public User User { get; set; }
        public Status Status { get; set; } // Статус связи пользователя и прав (например, "Активна", "Отменена")


    }
}
