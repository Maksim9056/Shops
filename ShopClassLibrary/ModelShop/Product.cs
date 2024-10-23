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

        public string Name_Product { get; set; }
        public string ProductsDescription { get; set; }

        public long ProductCount { get; set; }
        public long ProductPrice { get; set; }

        public Image Id_ProductDataImage { get; set; }

        public Category Category_Id { get; set; }
        public Status Status { get; set; } // Статус связи пользователя и прав (например, "Активна", "Отменена")

    }
}
