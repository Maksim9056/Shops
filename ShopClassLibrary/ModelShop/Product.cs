using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopClassLibrary.ModelShop
{
    public class Product
    {
        public long Id { get; set; }

        public string Products { get; set; }
        public string ProductsDescription { get; set; }

        public long ProductCount { get; set; }

        public byte[] ProductDataImage { get; set; }
        public Status Status { get; set; } // Статус связи пользователя и прав (например, "Активна", "Отменена")

    }
}
