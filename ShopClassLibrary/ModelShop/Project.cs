using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopClassLibrary.ModelShop
{
    public class Project
    {
        public long Id { get; set; }
        public string ProjectName { get; set; }
        public string ProjectVersion { get; set; }
        public string ProjectDescription { get; set; }

        // Дополнительные свойства
        public DateTime StartDate { get; set; } // Дата начала проекта
        public DateTime EndDate { get; set; } // Дата окончания проекта (может быть null для проектов без конкретного срока)
        public IEnumerable<Tasks_Shop> Tasks { get; set; } = new List<Tasks_Shop>(); // Связанные задачи

        public User ProjectManager { get; set; } // Менеджер проекта
        public Status Status { get; set; } // Статус связи пользователя и прав (например, "Активна", "Отменена")

    }

}
