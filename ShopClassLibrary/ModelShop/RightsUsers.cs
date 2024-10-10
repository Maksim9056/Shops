using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopClassLibrary.ModelShop
{
    public class RightsUsers
    {
        public long Id { get; set; }
        public User Id_User { get; set; }
        Rights Id_Rights { get; set; }
        public DateTime AssignedDate { get; set; } // Дата назначения прав
        public Status Status { get; set; } // Статус связи пользователя и прав (например, "Активна", "Отменена")

    }
}
