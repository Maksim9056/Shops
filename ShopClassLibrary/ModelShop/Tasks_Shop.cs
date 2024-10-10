using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopClassLibrary.ModelShop
{
    public class Tasks_Shop
    {
        public long Id { get; set; }
        public string TaskName { get; set; }

        // Дополнительные свойства
        public string Description { get; set; } // Описание задачи
        public DateTime DueDate { get; set; } // Срок выполнения
        public DateTime CreationDate { get; set; } // Дата создания задачи
        public Status Status { get; set; } // Статус связи пользователя и прав (например, "Активна", "Отменена")
        public User AssignedUser { get; set; } // Пользователь, назначенный на выполнение задачи

        public Project Project { get; set; } // Связанный проект

    }

}
