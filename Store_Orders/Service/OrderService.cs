using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopClassLibrary;
using ShopClassLibrary.ModelShop;

namespace Store_Orders.Service
{
    public class OrderService
    {
        private readonly ShopData _context;

        public OrderService(ShopData context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            var orders = await _context.Orders
             .Include(o => o.User.Id_User_Image.ImageCopies).Include(o => o.Status)
             .ToListAsync();
            return orders;
        }

        public async Task<Order> GetOrderByIdAsync(long id)
        {

            var order = await _context.Orders
                   .Include(o => o.User)
                   .FirstOrDefaultAsync(o => o.Id == id);
            return order;
        }

        public async Task<string> ProcessOrderPaymentAsync(Advance_Payment payment)
        {
            var user = await _context.Users
               .Include(u => u.Id_User_Image)
                   .ThenInclude(i => i.ImageCopies)
               .Include(u => u.Status)
               .FirstOrDefaultAsync(u => u.Id == payment.Id_User);
            long Orders_product = 0;

            // Извлекаем заказы, которые необходимо оплатить
            var ordersToPay = await _context.Orders.Include(u => u.Status)
                .Where(o => payment.SelectedOrderIds.Contains(o.Idproduct) && o.User.Id == user.Id)
                .ToListAsync();




            if (ordersToPay.Count != payment.SelectedOrderIds.Count)
            {
                return "Некоторые заказы не найдены или не принадлежат пользователю.";
            }
            long SUMM = 0;
            foreach (var i in payment.SelectedOrderIds)
            {

                var prodduct = await _context.Products.FirstOrDefaultAsync(u => u.Id == i);
                if (prodduct != null)
                {
                    SUMM = +prodduct.ProductPrice;

                }
            }

            // Рассчитываем общую стоимость заказов
            var totalAmount = SUMM;

            //// Проверяем, достаточно ли средств на счету
            //if (user.Money_Account < totalAmount)
            //{
            //    return "Недостаточно средств для оплаты заказов.";
            //}

            //// Списываем сумму со счета пользователя и обновляем статус заказов
            //user.Money_Account -= totalAmount;
            //foreach (var orders in ordersToPay)
            //{
            //    orders.Status = await _context.Status.FirstOrDefaultAsync(s => s.Description == "Заказ оплачен!");
            //}

            // Сохраняем изменения
            await _context.SaveChangesAsync();
            return "Оплата прошла успешно.";
        }

        public async Task<ActionResult<Order>> CreateOrderAsync(Order order)
        {
            var user = await _context.Users
                      .Include(u => u.Id_User_Image)
                      .ThenInclude(i => i.ImageCopies)
                      .Include(u => u.Status)
                      .FirstOrDefaultAsync(u => u.Id == order.User.Id);

            if (user == null)
            {
                return new BadRequestObjectResult("Пользователь не найден.");
            }

            bool orderExists = await _context.Orders
                .AnyAsync(o => o.Idproduct == order.Idproduct && o.User.Id == user.Id);

            if (orderExists)
            {
                return new BadRequestObjectResult("Товар уже добавлен у этого пользователя.");
            }

            var productStatus = await _context.Status.FirstOrDefaultAsync(s => s.Id == order.Status.Id);
            if (productStatus == null)
            {
                return new BadRequestObjectResult("Статус продукта не найден.");
            }

            order.User = user;
            order.Status = productStatus;

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return new OkObjectResult(order);
        }

        public async Task<bool> UpdateOrderAsync(long id, Order updatedOrder)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return false;
            }

            order.OrdersName = updatedOrder.OrdersName;
            order.Idproduct = updatedOrder.Idproduct;
            order.User = updatedOrder.User;
            order.Status = updatedOrder.Status;

            _context.Orders.Update(order);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteOrderAsync(long id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return false;
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
