using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopClassLibrary.ModelShop
{
    public class Rights
    {

        public long Id { get; set; }

        public string RightsName { get; set; }

        public Status Status { get; set; } // Статус связи пользователя и прав (например, "Активна", "Отменена")

    }
}
