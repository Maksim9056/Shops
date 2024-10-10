using ShopClassLibrary.ModelShop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopClassLibrary.Service
{
    public interface IOrderService : ICrudService<Order>
    {
        Task<IEnumerable<Order>> GetByUser(User user); // Получение заказов по пользователю
        Task ChangeStatus(long orderId, Status newStatus); // Изменение статуса заказа
    }

}
