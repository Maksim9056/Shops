using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopClassLibrary.ModelShop
{
    public class Status
    {
        public long Id { get; set; }
        public string StatusName { get; set; } // Название статуса (например, "Активный", "Завершен", "Ожидание")
        public string Description { get; set; } // Описание статуса (опционально)
    }

}
